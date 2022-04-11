using robotManager.Helpful;
using System.Collections.Generic;
using System.Threading;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace WholesomeToolbox
{
    /// <summary>
    /// Item related methods
    /// </summary>
    public class WTItem
    {
        /// <summary>
        /// Returns an item cooldown. An item off cooldown returns a negative value.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Item cooldown or 0 if the item is not found</returns>
        public static int GetItemCooldown(int itemId)
        {
            List<WoWItem> _bagItems = Bag.GetBagItem();
            foreach (WoWItem item in _bagItems)
                if (itemId == item.Entry)
                    return Lua.LuaDoString<int>($@"
                        local startTime, duration, _ = GetItemCooldown({itemId});
                        return duration - (GetTime() - startTime);
                    ");
            WTLogger.LogError($"Couldn't find item {itemId}");
            return 0;
        }

        /// <summary>
        /// Accepts a quest from an item located in the player's bags
        /// </summary>
        /// <param name="itemName"></param>
        public static void PickupQuestFromBagItem(string itemName)
        {
            ItemsManager.UseItemByNameOrId(itemName);
            Thread.Sleep(500);
            Lua.LuaDoString("if GetClickFrame('QuestFrame'):IsVisible() then AcceptQuest(); end");
            Thread.Sleep(500);
            Lua.LuaDoString(@"
                        local closeButton = GetClickFrame('QuestFrameCloseButton');
                        if closeButton:IsVisible() then
            	            closeButton:Click();
                        end");
        }

        /// <summary>
        /// Deletes an item from your bags
        /// </summary>
        /// <param name="itemName"></param>
        public static void DeleteItemByName(string itemName)
        {
            Lua.LuaDoString($@"
                for b=0,4 do 
                    for s=1,36 do 
                        n=GetContainerItemLink(b,s);
                        if n and string.find(n,""{itemName}"",1,true) then 
                            PickupContainerItem(b,s);
                            DeleteCursorItem();
                            return;
                        end;
                    end;
                end;
            ");
        }

        /// <summary>
        /// Returns an array of known mounts ids. Wotlk+ only
        /// </summary>
        /// <returns>Array of mounts ids</returns>
        public static int[] WotlKGetKnownMountsIds()
        {
            return Lua.LuaDoString<int[]>($@"
                local numComp  = GetNumCompanions('MOUNT');
                local result = {{}};
                for i=1, numComp, 1 do
                    local creatureID, creatureName, spellID, icon, active = GetCompanionInfo('MOUNT', i);
                    result[i] = spellID;
                end
                return unpack(result);
            ");
        }

        /// <summary>
        /// Equips a bag
        /// </summary>
        /// <param name="bagName"></param>
        public static void EquipBag(string bagName)
        {
            Lua.RunMacroText($"/equip {bagName}");
            Lua.LuaDoString($"EquipPendingItem(0);");
        }
    }
}
