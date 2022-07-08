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
        /// Returns an item cooldown in seconds. An item off cooldown returns a negative value.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Item cooldown in seconds, negative value if it's off cooldown, 0 if the item is not found</returns>
        public static int GetItemCooldown(int itemId)
        {
            List<WoWItem> _bagItems = Bag.GetBagItem();
            foreach (WoWItem item in _bagItems)
            {
                if (itemId == item.Entry)
                {
                    return Lua.LuaDoString<int>($@"
                            local startTime, duration, _ = GetItemCooldown({itemId});
                            return duration - (GetTime() - startTime);
                        ");
                }
            }
            WTLogger.LogError($"Couldn't find item {itemId}");
            return 0;
        }

        /// <summary>
        /// Returns an item cooldown in seconds. An item off cooldown returns a negative value.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Item cooldown in seconds, negative value if it's off cooldown, 0 if the item is not found</returns>
        public static int GetItemCooldown(string itemName)
        {
            List<WoWItem> _bagItems = Bag.GetBagItem();
            foreach (WoWItem item in _bagItems)
            {
                if (itemName == item.Name)
                {
                    return Lua.LuaDoString<int>($@"
                            local startTime, duration, _ = GetItemCooldown({item.Entry});
                            return duration - (GetTime() - startTime);
                        ");
                }
            }
            WTLogger.LogError($"Couldn't find item {itemName}");
            return 0;
        }

        /// <summary>
        /// Returns an item ID
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns>Bag item ID, 0 if not found</returns>
        public static int GetItemEntry(string itemName)
        {
            List<WoWItem> _bagItems = Bag.GetBagItem();
            foreach (WoWItem item in _bagItems)
                if (itemName.Equals(item.Name))
                    return item.Entry;
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
        /// Deletes the first item from your bags matching the specified name
        /// </summary>
        /// <param name="itemName"></param>
        public static void DeleteItemByName(string itemName)
        {
            Lua.LuaDoString($@"
                for b=0,4 do 
                    for s=1,36 do 
                        n=GetContainerItemLink(b,s);
                        if n and string.find(n,""{itemName.EscapeLuaString()}"",1,true) then 
                            PickupContainerItem(b,s);
                            DeleteCursorItem();
                            return;
                        end;
                    end;
                end;
            ");
        }

        /// <summary>
        /// Deletes all the items from your bags matching the specified name
        /// </summary>
        /// <param name="itemName"></param>
        public static void DeleteAllItemsByName(string itemName)
        {
            Lua.LuaDoString($@"
                for b=0,4 do 
                    for s=1,36 do 
                        n=GetContainerItemLink(b,s);
                        if n and string.find(n,""{itemName.EscapeLuaString()}"",1,true) then 
                            PickupContainerItem(b,s);
                            DeleteCursorItem();
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

        /// <summary>
        /// Counts the amount of the specified item stacks in your bags
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns>amount of item stacks</returns>
        public static int CountItemStacks(string itemName) => Lua.LuaDoString<int>($@"return GetItemCount(""{itemName.EscapeLuaString()}"");");

        /// <summary>
        /// Checks if you have any of the listed items in your bags
        /// </summary>
        /// <param name="list"></param>
        /// <returns>true if you have any of the listed items in your bags</returns>
        public static bool HaveOneInList(List<string> list)
        {
            foreach (WoWItem item in Bag.GetBagItem())
            {
                if (list.Contains(item.Name))
                    return true;
            }
            return false;
        }

        public static int GetNbItems(string itemName) => Lua.LuaDoString<int>($@"return GetItemCount(""{itemName.EscapeLuaString()}"");");
    }
}
