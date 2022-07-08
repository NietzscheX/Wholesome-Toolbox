using System;
using System.Threading;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Combat related methods
    /// </summary>
    public class WTCombat
    {
        /// <summary>
        /// Accepts resurrection request after a random time between 1 and 2 sec
        /// </summary>
        public static void AcceptResurrect()
        {
            int timer = new Random().Next(1000, 2000);
            WTLogger.Log($"Accepting resurrection request in {timer} ms");
            Thread.Sleep(timer);
            Lua.RunMacroText("/script AcceptResurrect(); StaticPopup1Button1: Click(\"left\", true);");
        }

        /// <summary>
        /// Returns whether a player spell is repeating (ex: 5019)
        /// </summary>
        /// <returns>true if the player is using a wand</returns>
        public static bool IsSpellRepeating(int spellId)
        {
            return Lua.LuaDoString<bool>($@"
                local name = GetSpellInfo({spellId});
                return IsAutoRepeatSpell(name) ~= nil;
            ");
        }

        /// <summary>
        /// Returns whether a player spell is repeating (ex: Wand)
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns></returns>
        public static bool IsSpellRepeating(string spellName)
        {
            return Lua.LuaDoString<bool>($@"
                return IsAutoRepeatSpell(""{spellName.EscapeLuaString()}"") ~= nil;
            ");
        }

        /// <summary>
        /// Returns whether a spell is active (ex: Attack)
        /// </summary>
        /// <param name="spellId"></param>
        /// <returns>true if the spell is active</returns>
        public static bool IsSpellActive(int spellId)
        {
            return Lua.LuaDoString<bool>($@"
                local name = GetSpellInfo({spellId});
                return IsCurrentSpell(name) ~= nil;
            ");
        }

        /// <summary>
        /// Returns whether a spell is active (ex: Attack)
        /// </summary>
        /// <param name="spellId"></param>
        /// <returns>true if the spell is active</returns>
        public static bool IsSpellActive(string spellName)
        {
            return Lua.LuaDoString<bool>($@"
                return IsCurrentSpell(""{spellName.EscapeLuaString()}"") ~= nil;
            ");
        }

        /// <summary>
        /// Returns whether the target is either casting or channeling (good for interrupts)
        /// </summary>
        /// <returns>true if the target is casting or channeling</returns>
        public static bool TargetIsCasting()
        {
            return GetChannelTimeLeft("target") > 0 || ObjectManager.Target.CastingTimeLeft > 0;
        }

        /// <summary>
        /// Returns the remaining channel time on a unit in milliseconds
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>Channel time left in milliseconds</returns>
        public static int GetChannelTimeLeft(string unit)
        {
            return Lua.LuaDoString<int>($@"
                    local spell, _, _, _, startTimeMS, endTimeMS = UnitChannelInfo(""{unit.EscapeLuaString()}"")
                    if spell then
                        return endTimeMS - GetTime() * 1000
                    end
                ");
        }

        /// <summary>
        /// Returns a spell cooldown
        /// </summary>
        /// <param name="spellName"></param>
        /// <returns>Spell cooldown in milliseconds, negative if off cooldown, 0 if not found</returns>
        public static int GetSpellCooldown(string spellName)
        {
            return Lua.LuaDoString<int>($@"
                    local startTime, duration, _ = GetSpellCooldown(""{spellName.EscapeLuaString()}"");
                    if (startTime == nil) then
                        return 0;
                    end
                    return (duration - (GetTime() - startTime)) * 1000;
                ");

        }
    }
}
