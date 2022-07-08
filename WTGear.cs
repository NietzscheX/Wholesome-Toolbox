using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Gear (Inventory sheet) related methods
    /// </summary>
    public class WTGear
    {
        /// <summary>
        /// Returns the player's gear average durability of all his equipment in percentage
        /// </summary>
        /// <returns>Durability percentage</returns>
        public static int GetAvergaeDurability()
        {
            return Lua.LuaDoString<int>($@"
                local avrgDurability = 0;
                local nbItems = 0;
                for i=1,20 do
                    local durability, max = GetInventoryItemDurability(i);
                    if durability ~= nil and max ~= nil then
                        avrgDurability = avrgDurability + durability;
                        nbItems = nbItems + 1;
                    end
                end

                if nbItems > 0 then
                    return avrgDurability / nbItems;
                else 
                    return 100;
                end
            ");
        }

        /// <summary>
        /// Gets the type of main hand weapon of the unit (default to player) ex : Daggers
        /// </summary>
        /// <param name="unit"></param>
        /// <returns>weapon type</returns>
        public static string GetMainHandWeaponType(string unit = "player")
        {
            return Lua.LuaDoString<string>($@"
                    local _, _, _, _, _, _, weapontype = GetItemInfo(GetInventoryItemLink(""{unit.EscapeLuaString()}"", 16)); 
                    return weapontype;"
                );
        }

        /// <summary>
        /// Returns true if the player has an offhand weapon equipped
        /// </summary>
        public static bool HaveOffHandWeaponEquipped => Lua.LuaDoString<bool>("return OffhandHasWeapon()");

        /// <summary>
        /// Returns whether the player has an enchant or poison on their main hand weapon
        /// </summary>
        /// <returns>true if the weapon has an enchant</returns>
        public static bool HaveMainHandEnchant()
        {
            return Lua.LuaDoString<bool>($@"
                local hasMainHandEnchant = GetWeaponEnchantInfo();
                return hasMainHandEnchant;
            ");
        }

        /// <summary>
        /// Returns whether the player has an enchant or poison on their off hand weapon
        /// </summary>
        /// <returns>true if the weapon has an enchant</returns>
        public static bool HaveOffHandEnchant()
        {
            return Lua.LuaDoString<bool>($@"
                local _, _, _, _, hasOffHandEnchant = GetWeaponEnchantInfo();
                return hasOffHandEnchant;
            ");
        }

        /// <summary>
        /// Check if the player has a range weapon equipped (wand, bow, gun)
        /// </summary>
        /// <returns>true if the player has a ranged weapon equipped</returns>
        public static bool HaveRangedWeaponEquipped => ObjectManager.Me.GetEquipedItemBySlot(wManager.Wow.Enums.InventorySlot.INVSLOT_RANGED) != 0;
    }
}
