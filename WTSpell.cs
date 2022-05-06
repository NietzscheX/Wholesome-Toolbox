using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Spell related methods
    /// </summary>
    public class WTSpell
    {
        /// <summary>
        /// Returns the id of the max rank of the spell
        /// </summary>
        /// <returns>spell (max rank) id</returns>
        public static uint GetId(string spellName, int rank)
        {
            string rankName = rank == 0 ? "" : $"Rank {rank}";
            return Lua.LuaDoString<uint>($@"
                local spellLink = GetSpellLink(""{spellName.EscapeLuaString()}"", ""{rankName}"");
                if spellLink then
                    local _, _, result = string.find(spellLink, ""Hspell:(%d+)|"");
                    return result;
                end
                return 0;
            ");
        }
    }
}
