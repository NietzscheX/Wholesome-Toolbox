using System.Collections.Generic;
using System.Linq;
using System.Threading;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Talent related methods
    /// </summary>
    public class WTTalent
    {
        /// <summary>
        /// Returns the amount of points assigned to a specific talent
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="talentIndex"></param>
        /// <returns>Talent rank</returns>
        public static int GetTalentRank(int tabIndex, int talentIndex)
            => Lua.LuaDoString<int>($"local _, _, _, _, currentRank, _, _, _ = GetTalentInfo({tabIndex}, {talentIndex}); return currentRank;");

        /// <summary>
        /// Returns Character's specialization (by Marsbar) Modified to return 0 if all talent trees have 0 point
        /// </summary>
        /// <returns>Specialization tree number</returns>
        public static int GetSpec()
        {
            var Talents = new Dictionary<int, int>();
            for (int i = 0; i <= 3; i++)
            {
                Talents.Add(
                    i,
                    Lua.LuaDoString<int>($"local _, _, pointsSpent = GetTalentTabInfo({i}); return pointsSpent")
                );
            }
            int highestTalents = Talents.Max(x => x.Value);
            return Talents.Where(t => t.Value == highestTalents).FirstOrDefault().Key;
        }

        public static int NbUnspentTalentPoints =>
            Lua.LuaDoString<int>("local unspentTalentPoints, _ = UnitCharacterPoints('player'); return unspentTalentPoints;");

        /// <summary>
        /// Talent object
        /// </summary>
        private class ToolboxTalent
        {
            public readonly string Name;
            public readonly int CurrentRank;
            public readonly int MaxRank;
            public readonly int TalentTree;
            public ToolboxTalent(int talentTree, string name, int currentRank, int maxRank)
            {
                TalentTree = talentTree;
                Name = name;
                CurrentRank = currentRank;
                MaxRank = maxRank;
            }
        }

        /// <summary>
        /// Gets a talent object
        /// </summary>
        /// <param name="talentTree"></param>
        /// <param name="talentIndex"></param>
        /// <returns>Talent object or null if talent not found</returns>
        private static ToolboxTalent GetTalentObject(int talentTree, int talentIndex)
        {
            string[] result = Lua.LuaDoString<string[]>($@"
                local result = {{}};
                local name, _, _, _, currentRank, maxRank, _, _ = GetTalentInfo({talentTree}, {talentIndex});
                result[1] = name;
                result[2] = currentRank;
                result[3] = maxRank;
                return unpack(result);
            ");
            if (string.IsNullOrEmpty(result[0]))
            {
                return null;
            }
            string name = result[0];
            int currentRank = int.Parse(result[1]);
            int maxRank = int.Parse(result[2]);
            return new ToolboxTalent(talentTree, name, currentRank, maxRank);
        }

        /// <summary>
        /// Returns the amount of talents in each tree
        /// </summary>
        /// <returns>an array containing the amount of talents in each tree</returns>
        public static int[] NumTalentsInTrees()
        {
            return Lua.LuaDoString<int[]>($@"
                local result = {{}};
                result[1] = GetNumTalents(1);
                result[2] = GetNumTalents(2);
                result[3] = GetNumTalents(3);
                return unpack(result);
            ");
        }

        /// <summary>
        /// Assign talent points using a list of numeric talent codes. TBC only.
        /// </summary>
        /// <param name="talentCodes"></param>
        public static void TBCAssignTalents(List<string> talentCodes)
        {
            if (ObjectManager.Me.Level < 10)
                return;

            int[] numTalentsInTrees = NumTalentsInTrees();

            if (talentCodes.Count() <= 0)
            {
                WTLogger.LogError("No talent code");
                return;
            }

            // Loop for each TalentCode in list
            foreach (string talentsCode in talentCodes)
            {
                // check if talent code length is correct
                if ((numTalentsInTrees[0] + numTalentsInTrees[1] + numTalentsInTrees[2]) != talentsCode.Length)
                {
                    WTLogger.LogError($@"WARNING: Your talents code length is incorrect. Please use
                            http://armory.twinstar.cz/talent-calc.php to generate valid codes.");
                    WTLogger.LogError($"Talents code: {talentsCode}");
                    return;
                }

                // TalentCode per tree
                List<string> TalentCodeTrees = new List<string>()
                    {
                        talentsCode.Substring(0, numTalentsInTrees[0]),
                        talentsCode.Substring(numTalentsInTrees[0], numTalentsInTrees[1]),
                        talentsCode.Substring(numTalentsInTrees[0] + numTalentsInTrees[1], numTalentsInTrees[2])
                    };

                // loop in 3 trees
                for (int k = 1; k <= 3; k++)
                {
                    // loop for each talent
                    for (int i = 0; i < numTalentsInTrees[k - 1]; i++)
                    {
                        int talentNumber = i + 1;
                        ToolboxTalent talent = GetTalentObject(k, talentNumber);

                        if (talent == null)
                        {
                            WTLogger.LogError($"Talent {talentNumber} in tree {k} was't found");
                            return;
                        }

                        int _pointsToAssignInTalent;
                        if (int.TryParse(TalentCodeTrees[k - 1].Substring(i, 1), out int points))
                        {
                            _pointsToAssignInTalent = points;
                        }
                        else
                        {
                            WTLogger.LogError($@"WARNING: A character is not a number in your 
                                    talent code {TalentCodeTrees[k - 1]}");
                            return;
                        }

                        if (_pointsToAssignInTalent <= 0)
                            return;

                        if (talent.CurrentRank > _pointsToAssignInTalent && talentCodes.Last().Equals(talentsCode))
                        {
                            WTLogger.LogError($@"WARNING: Your assigned talent points don't match your talent code. 
                                    Please reset your talents or review your talents code. 
                                    You have {talent.CurrentRank} point(s) in {talent.Name} 
                                    where you should have {_pointsToAssignInTalent} point(s)");
                            WTLogger.LogError($"Talents code: {talentsCode}");
                            return;
                        }
                        else if (_pointsToAssignInTalent > talent.MaxRank)
                        {
                            WTLogger.LogError($@"WARNING: You're trying to assign {_pointsToAssignInTalent} 
                                    points into {talent.Name},
                                    maximum is {talent.MaxRank} points for this talent. 
                                    Please check your talent code.");
                            WTLogger.LogError($"Talents code: {talentsCode}");
                            return;
                        }
                        else if (talent.CurrentRank != _pointsToAssignInTalent)
                        {
                            // loop for individual talent rank
                            for (int j = 0; j < _pointsToAssignInTalent - talent.CurrentRank; j++)
                            {
                                Lua.LuaDoString($"LearnTalent({k}, {talentNumber})");
                                Thread.Sleep(500 + Usefuls.Latency);
                                WTLogger.Log($@"Assigned talent: {talent.Name}: {GetTalentRank(k, talentNumber)}/{ _pointsToAssignInTalent}");
                                if (NbUnspentTalentPoints <= 0)
                                    return;
                            }
                        }
                    }
                }
            }
        }
    }
}
