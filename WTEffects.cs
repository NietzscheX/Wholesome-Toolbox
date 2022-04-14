using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Effects related methods (buffs, debuffs etc..)
    /// </summary>
    public class WTEffects
    {
        /// <summary>
        /// Returns whether a unit has the debuff passed as a stringon a unit (default to player) (ex: Weakened Soul, or Resurrection Sickness)
        /// </summary>
        /// <param name="debuffName"></param>
        /// <param name="unitName"></param>
        /// <param name="loops"></param>
        /// <returns>true if the unit has the specified debuff</returns>
        public static bool HasDebuff(string debuffName, string unitName = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                ($@"for i=1,{loops} do
                    local n, _, _, _, _  = UnitDebuff(""{unitName.EscapeLuaString()}"",i);
                    if n == ""{debuffName.EscapeLuaString()}"" then
                        return true
                    end
                end");
        }

        /// <summary>
        /// Cancels a player buff (TBC only)
        /// </summary>
        /// <param name="buffName"></param>
        public static void TBCCancelPlayerBuff(string buffName)
        {
            WTLogger.Log($"Removing buff {buffName}");
            Lua.LuaDoString($@"CancelPlayerBuff(""{buffName.EscapeLuaString()}"")");
        }

        /// <summary>
        /// Returns whether a unit (default to player) has a poison debuff
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>true if the unit has a poison debuff</returns>
        public static bool HasPoisonDebuff(string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                (@$"for i=1,{loops} do 
	                    local _, _, _, _, d  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
	                    if d == 'Poison' then
                            return true
                        end
                    end");
        }

        /// <summary>
        /// Returns whether a unit (default to player) has a disease debuff
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>true if the unit has a disease debuff</returns>
        public static bool HasDiseaseDebuff(string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                (@$"for i=1,{loops} do 
	                    local _, _, _, _, d  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
	                    if d == 'Disease' then
                            return true
                        end
                    end");
        }

        /// <summary>
        /// Returns whether a unit (default to player) has a curse debuff
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>true if the unit has a curse debuff</returns>
        public static bool HasCurseDebuff(string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                (@$"for i=1,{loops} do 
	                    local _, _, _, _, d  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
	                    if d == 'Curse' then
                            return true
                        end
                    end");
        }

        /// <summary>
        /// Returns whether a unit (default to player) has a magic debuff
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>true if the unit has a magic debuff</returns>
        public static bool HasMagicDebuff(string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<bool>
                (@$"for i=1,{loops} do 
	                    local _, _, _, _, d  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
	                    if d == 'Magic' then
                            return true
                        end
                    end");
        }

        /// <summary>
        /// Returns the amount of stacks of a specific buff on a unit (default to player) ex: Lifebloom
        /// </summary>
        /// <param name="buffName"></param>
        /// <param name="unit"></param>
        /// <returns>Amount of buff stacks</returns>
        public static int CountBuffStacks(string buffName, string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<int>
                (@$"for i=1,{loops} do
                        local n, _, _, c, _  = UnitBuff(""{unit.EscapeLuaString()}"",i);
                        if n == ""{buffName.EscapeLuaString()}"" then
                            return c
                        end
                    end");
        }

        /// <summary>
        /// Returns the time left of a buff in seconds on a unit (default to player)
        /// </summary>
        /// <param name="buffName"></param>
        /// <param name="unit"></param>
        /// <returns>Time left in seconds</returns>
        public static int BuffTimeLeft(string buffName, string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<int>
                (@$"for i=1,{loops} do
                        local n, _, _, _, _, duration, _  = UnitBuff(""{unit.EscapeLuaString()}"",i);
                        if n == ""{buffName.EscapeLuaString()}"" then
                            return duration
                        end
                    end");
        }

        /// <summary>
        /// Returns the time left on a debuff in seconds on a unit (default to player)
        /// </summary>
        /// <param name="debuffName"></param>
        /// <param name="unit"></param>
        /// <returns>Time left in seconds</returns>
        public static int DeBuffTimeLeft(string debuffName, string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<int>
                (@$"for i=1,{loops} do
                        local n, _, _, _, _, _, expirationTime  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
                        if n == ""{debuffName.EscapeLuaString()}"" then
                            return expirationTime
                        end
                    end");
        }

        /// <summary>
        /// Returns the amount of stacks of a specific debuff on a unit (default to player) (ex: Arcane Blast)
        /// </summary>
        /// <param name="debuffName"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static int CountDebuff(string debuffName, string unit = "player", int loops = 25)
        {
            return Lua.LuaDoString<int>
                (@$"for i=1,{loops} do
                        local n, _, _, c, _  = UnitDebuff(""{unit.EscapeLuaString()}"",i);
                        if n == ""{debuffName.EscapeLuaString()}"" then
                            return c
                        end
                    end");
        }
    }
}
