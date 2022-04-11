using wManager.Wow.Helpers;

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
    }
}
