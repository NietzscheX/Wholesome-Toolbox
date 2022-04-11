using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;

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
    }
}
