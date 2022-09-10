using robotManager.Helpful;
using System.Collections.Generic;
using wManager;
using wManager.Wow.Enums;
using static wManager.Wow.Helpers.PathFinder;

namespace WholesomeToolbox
{
    /// <summary>
    /// WRobot settings related methods
    /// </summary>
    public class WTSettings
    {
        /// <summary>
        /// Adds an item to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void AddToDoNotSellList(string itemName)
        {
            if (!wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
            {
                wManagerSetting.CurrentSetting.DoNotSellList.Add(itemName);
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Adds items to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void AddToDoNotSellList(List<string> itemNames)
        {
            foreach (string itemName in itemNames)
            {
                if (!wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    wManagerSetting.CurrentSetting.DoNotSellList.Add(itemName);
                }
            }
            wManagerSetting.CurrentSetting.Save();
        }


        /// <summary>
        /// Removes an item to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void RemoveFromDoNotSellList(string itemName)
        {
            if (wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
            {
                wManagerSetting.CurrentSetting.DoNotSellList.Remove(itemName);
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Remove items from the DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void RemoveFromDoNotSellList(List<string> itemNames)
        {
            foreach (string itemName in itemNames)
            {
                if (wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    wManagerSetting.CurrentSetting.DoNotSellList.Remove(itemName);
                }
            }
            wManagerSetting.CurrentSetting.Save();
        }

        /// <summary>
        /// Add a zone to the blacklist
        /// </summary>
        /// <param name="location"></param>
        /// <param name="radius"></param>
        /// <param name="continent"></param>
        /// <param name="isSessionBlacklist"></param>
        /// <param name="force"></param>
        public static void AddBlacklistZone(Vector3 location, int radius, ContinentId continent, bool isSessionBlacklist = true, bool force = false)
        {
            if (force || !wManagerSetting.IsBlackListedZone(location))
            {
                wManagerSetting.AddBlackListZone(location, radius, continent, isSessionBlacklist: isSessionBlacklist);
            }
        }

        /// <summary>
        /// Adds recommended blacklist zones
        /// </summary>
        public static void AddRecommendedBlacklistZones()
        {
            if (OffMeshConnections.MeshConnection == null || OffMeshConnections.MeshConnection.Count <= 0)
            {
                OffMeshConnections.Load();
            }
            // Faction specific
            if (WTPlayer.IsHorde())
            {
                // Astranaar
                AddBlacklistZone(new Vector3(2735.73, -373.2593, 107.1535), 160, ContinentId.Kalimdor);
            }
            else
            {
                // Crossroads
                AddBlacklistZone(new Vector3(-452.84, -2650.76, 95.5209), 160, ContinentId.Kalimdor);
                // BrackenWall Village
                AddBlacklistZone(new Vector3(-3124.758, -2882.661, 34.73463), 130, ContinentId.Kalimdor);
            }
            // Drak'Tharon Keep
            AddBlacklistZone(new Vector3(4643.429, -2043.915, 184.1842), 200, ContinentId.Northrend);
            // Blue sky logging camp water
            AddBlacklistZone(new Vector3(4321.85, -3021.175, 305.8569), 50, ContinentId.Northrend);
            // Avoid Orgrimmar Braseros
            AddBlacklistZone(new Vector3(1731.702, -4423.403, 36.86293), 5, ContinentId.Kalimdor);
            AddBlacklistZone(new Vector3(1669.99, -4359.609, 29.23425), 5, ContinentId.Kalimdor);
            // Warsong hold top elevator
            AddBlacklistZone(new Vector3(2892.18, 6236.34, 208.908), 15, ContinentId.Northrend);
            // Portal Rut'Theran UP/DOWN
            AddBlacklistZone(new Vector3(9946.391, 2630.067, 1316.194), 15, ContinentId.Kalimdor);
            AddBlacklistZone(new Vector3(8798.752, 969.5687, 30.38474), 15, ContinentId.Kalimdor);
            // Staghein Point
            AddBlacklistZone(new Vector3(-6427.419, 219.1993, 4.853653), 70, ContinentId.Kalimdor);
            // Hellfire giants passage
            AddBlacklistZone(new Vector3(41.35702, 4443.034, 81.65746), 70, ContinentId.Expansion01);
            // Telredor base
            AddBlacklistZone(new Vector3(283.2617, 6052.715, 23.4), 60, ContinentId.Expansion01);
            // Shadowmoon pool
            AddBlacklistZone(new Vector3(-4204.122, 1712.808, 88.00595), 60, ContinentId.Expansion01);
            AddBlacklistZone(new Vector3(-4189.208, 2012.61, 57.39383), 50, ContinentId.Expansion01);
            // Sparksocket Minefield
            AddBlacklistZone(new Vector3(6084.664, -649.5412, 375.806), 50, ContinentId.Northrend);
            AddBlacklistZone(new Vector3(6037.082, -655.2501, 369.9831), 50, ContinentId.Northrend);
            wManagerSetting.CurrentSetting.Save();
        }

        /// <summary>
        /// Adds recommended offmeh connections
        /// </summary>
        public static void AddRecommendedOffmeshConnections()
        {
            // Ratchet northern house
            OffMeshConnections.Add(new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-859.9623, -3757.758, 19.88704, "None"),
                new Vector3(-847.8208, -3740.918, 22.26572, "None")
            }, (int)ContinentId.Kalimdor, OffMeshConnectionType.Bidirectional, true));
            // Sporeggar house
            OffMeshConnections.Add(new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(206.3829, 8499.533, 24.57104, "None"),
                new Vector3(194.8526, 8489.873, 27.46523, "None")
            }, (int)ContinentId.Expansion01, OffMeshConnectionType.Bidirectional, true));
            // Bootay bay water
            OffMeshConnections.Add(new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-14427.64, 462.951, -1.862247, "None"),
                new Vector3(-14439.08, 455.1722, 3.748821, "None")
            }, (int)ContinentId.Azeroth, OffMeshConnectionType.Bidirectional, true));

            OffMeshConnections.Save();
        }

        /// <summary>
        /// Set WRobot setting Ground Mount
        /// </summary>
        /// <param name="mountName"></param>
        public static void SetGroundMount(string mountName)
        {
            wManagerSetting.CurrentSetting.GroundMountName = mountName;
            wManagerSetting.CurrentSetting.Save();
            WTLogger.Log($"Setting ground mount to {mountName}");
        }

        /// <summary>
        /// Set WRobot setting Flying Mount
        /// </summary>
        /// <param name="mountName"></param>
        public static void SetFlyingMount(string mountName)
        {
            wManagerSetting.CurrentSetting.FlyingMountName = mountName;
            wManagerSetting.CurrentSetting.Save();
            WTLogger.Log($"Setting flying mount to {mountName}");
        }
    }
}
