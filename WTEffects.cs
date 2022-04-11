using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Effects related methods (buffs, debuffs etc..)
    /// </summary>
    public class WTEffects
    {
        /// <summary>
        /// Returns whether a unit has the debuff passed as a string (ex: Weakened Soul, or Resurrection Sickness)
        /// </summary>
        /// <param name="debuffName"></param>
        /// <param name="unitName"></param>
        /// <param name="loops"></param>
        /// <returns>true if the unit has the specified debuff</returns>
        public static bool HasDebuff(string debuffName, string unitName = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                ($@"for i=1,{loops} do
                    local n, _, _, _, _  = UnitDebuff('{unitName}',i);
                    if n == '{debuffName}' then
                    return true
                    end
                end");
        }
    }
}
