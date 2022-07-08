using robotManager.Helpful;
using System;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Methods related to locations and maps
    /// </summary>
    public class WTLocation
    {
        /// <summary>
        /// Returns the zone text displayed on the minimap (ex: Depprun Tramway)
        /// </summary>
        /// <returns>zone text</returns>
        public static string GetMinimapZoneText => Lua.LuaDoString<string>("return GetMinimapZoneText();");

        /// <summary>
        /// Returns the real zone text
        /// </summary>
        public static string GetRealZoneText => Lua.LuaDoString<string>("return GetRealZoneText();");

        /// <summary>
        /// Returns whether the player is in any starting zone
        /// </summary>
        /// <returns></returns>
        public static bool ZoneIsInAStartingZone()
        {
            string zone = GetRealZoneText;
            return ZoneInBloodElfStartingZone(zone)
                || ZoneInDraneiStartingZone(zone)
                || ZoneInDwarfStartingZone(zone)
                || ZoneInElfStartingZone(zone)
                || ZoneInHumanStartingZone(zone)
                || ZoneInOrcStartingZone(zone)
                || ZoneInTaurenStartingZone(zone)
                || ZoneInUndeadStartingZone(zone);
        }

        /// <summary>
        /// Returns whether the player is in the Blood elf starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInBloodElfStartingZone(string zone) => zone == "Eversong Woods" || zone == "Ghostlands" || zone == "Silvermoon City";

        /// <summary>
        /// Returns whether the player is in the Draenei starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInDraneiStartingZone(string zone) => zone == "Azuremyst Isle" || zone == "Bloodmyst Isle" || zone == "The Exodar";

        /// <summary>
        /// Returns whether the player is in the Orc/Troll starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInOrcStartingZone(string zone) => zone == "Durotar" || zone == "Orgrimmar";

        /// <summary>
        /// Returns whether the player is in the Tauren starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInTaurenStartingZone(string zone) => zone == "Mulgore" || zone == "Thunder Bluff";

        /// <summary>
        /// Returns whether the player is in the Undead starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInUndeadStartingZone(string zone) => zone == "Tirisfal Glades" || zone == "Undercity";

        /// <summary>
        /// Returns whether the player is in the Dwarf/Gnome starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInDwarfStartingZone(string zone) => zone == "Dun Morogh" || zone == "Ironforge";

        /// <summary>
        /// Returns whether the player is in the Human starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInHumanStartingZone(string zone) => zone == "Elwynn Forest" || zone == "Stormwind";

        /// <summary>
        /// Returns whether the player is in the Night Elf starting zone
        /// </summary>
        /// <param name="zone"></param>
        public static bool ZoneInElfStartingZone(string zone) => zone == "Darnassus" || zone == "Teldrassil";        

        /// <summary>
        /// Returns whether the player is in Outlands
        /// </summary>
        /// <returns>true if the player is in Outlands</returns>
        public static bool PlayerInOutlands()
        {
            string zone = GetRealZoneText;
            return (ContinentId)Usefuls.ContinentId == ContinentId.Expansion01
                && !ZoneInBloodElfStartingZone(zone)
                && !ZoneInDraneiStartingZone(zone);
        }

        /// <summary>
        /// Returns the Z differential between you and a location.
        /// </summary>
        /// <param name="checkPosition"></param>
        /// <returns>Z differential in yards</returns>
        public static float GetZDifferential(Vector3 checkPosition)
        {
            Vector3 myPos = ObjectManager.Me.Position;
            if (checkPosition.Z > myPos.Z) return checkPosition.Z - myPos.Z;
            else return myPos.Z - checkPosition.Z;
        }

        /// <summary>
        /// Returns whether the player is in an instance
        /// </summary>
        /// <returns>true if player is in an instance</returns>
        public static bool IsInInstance()
        {
            return Lua.LuaDoString<bool>($@"
                    local isInstance, instanceType = IsInInstance();
                    return instanceType ~= 'none';
                ");
        }

        /// <summary>
        /// Returns the player's corpse position
        /// </summary>
        /// <param name="corpse"></param>
        /// <returns>The player's corpse Vector3</returns>
        public static Vector3 GetCorpsePosition(WoWCorpse corpse)
        {
            try
            {
                uint baseAddress = corpse.GetBaseAddress;
                const ushort positionOffset = 0xE8;
                float x = wManager.Wow.Memory.WowMemory.Memory.ReadFloat(baseAddress + positionOffset + 0x00);
                float y = wManager.Wow.Memory.WowMemory.Memory.ReadFloat(baseAddress + positionOffset + 0x04);
                float z = wManager.Wow.Memory.WowMemory.Memory.ReadFloat(baseAddress + positionOffset + 0x08);
                return new Vector3(x, y, z);
            }
            catch (Exception e)
            {
                WTLogger.LogError("Failed to read corpse position: " + e.Message);
            }
            return new Vector3(0, 0, 0);
        }
    }
}
