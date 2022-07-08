using wManager.Wow.Enums;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Player related methods
    /// </summary>
    public class WTPlayer
    {
        /// <summary>
        /// Returns whether the player is horde
        /// </summary>
        /// <returns>true if player is horde</returns>
        public static bool IsHorde()
        {
            uint myFaction = ObjectManager.Me.Faction;
            return myFaction == (uint)PlayerFactions.Orc || myFaction == (uint)PlayerFactions.Tauren
                || myFaction == (uint)PlayerFactions.Undead || myFaction == (uint)PlayerFactions.BloodElf
                || myFaction == (uint)PlayerFactions.Troll;
        }
    }
}
