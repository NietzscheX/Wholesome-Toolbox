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
        /// Adds items to the DoNotMailList and DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void AddItemToDoNotSellAndMailList(List<string> itemNames)
        {
            bool newSettings = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (!wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    WTLogger.Log($"Adding {itemName} to DoNotSell list");
                    wManagerSetting.CurrentSetting.DoNotSellList.Add(itemName);
                    newSettings = true;
                }
                if (!wManagerSetting.CurrentSetting.DoNotMailList.Contains(itemName))
                {
                    WTLogger.Log($"Adding {itemName} to DoNotMail list");
                    wManagerSetting.CurrentSetting.DoNotMailList.Add(itemName);
                    newSettings = true;
                }
            }

            if (newSettings)
            {
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Removes items from the DoNotMailList and DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void RemoveItemFromDoNotSellAndMailList(List<string> itemNames)
        {
            bool newSettings = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    WTLogger.Log($"Removing {itemName} from DoNotSell list");
                    wManagerSetting.CurrentSetting.DoNotSellList.Remove(itemName);
                    newSettings = true;
                }
                if (wManagerSetting.CurrentSetting.DoNotMailList.Contains(itemName))
                {
                    WTLogger.Log($"Removing {itemName} from DoNotMail list");
                    wManagerSetting.CurrentSetting.DoNotMailList.Remove(itemName);
                    newSettings = true;
                }
            }

            if (newSettings)
            {
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Adds an item to the DoNotMailList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void AddToDoNotMailList(string itemName)
        {
            AddToDoNotMailList(new List<string>() { itemName });
        }

        /// <summary>
        /// Adds items to the DoNotMailList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void AddToDoNotMailList(List<string> itemNames)
        {
            bool settingsChanged = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (!wManagerSetting.CurrentSetting.DoNotMailList.Contains(itemName))
                {
                    WTLogger.Log($"Adding {itemName} to DoNotMail list");
                    wManagerSetting.CurrentSetting.DoNotMailList.Add(itemName);
                    settingsChanged = true;
                }
            }

            if (settingsChanged)
            {
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Removes an item to the DoNotMailList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void RemoveFromDoNotMailList(string itemName)
        {
            RemoveFromDoNotMailList(new List<string>() { itemName });
        }

        /// <summary>
        /// Remove items from the DoNotMailList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void RemoveFromDoNotMailList(List<string> itemNames)
        {
            bool settingsChanged = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    WTLogger.Log($"Removing {itemName} from DoNotMail list");
                    wManagerSetting.CurrentSetting.DoNotSellList.Remove(itemName);
                    settingsChanged = true;
                }
            }

            if (settingsChanged)
            {
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Adds an item to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void AddToDoNotSellList(string itemName)
        {
            AddToDoNotSellList(new List<string>() { itemName });
        }

        /// <summary>
        /// Adds items to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void AddToDoNotSellList(List<string> itemNames)
        {
            bool settingsChanged = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (!wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    WTLogger.Log($"Adding {itemName} to DoNotSell list");
                    wManagerSetting.CurrentSetting.DoNotSellList.Add(itemName);
                    settingsChanged = true;
                }
            }

            if (settingsChanged)
            {
                wManagerSetting.CurrentSetting.Save();
            }
        }

        /// <summary>
        /// Removes an item to the DoNotSellList and saves
        /// </summary>
        /// <param name="itemName"></param>
        public static void RemoveFromDoNotSellList(string itemName)
        {
            RemoveFromDoNotSellList(new List<string>() { itemName });
        }

        /// <summary>
        /// Remove items from the DoNotSellList and saves
        /// </summary>
        /// <param name="itemNames"></param>
        public static void RemoveFromDoNotSellList(List<string> itemNames)
        {
            bool settingsChanged = false;
            foreach (string itemName in itemNames)
            {
                if (string.IsNullOrEmpty(itemName)) continue;

                if (wManagerSetting.CurrentSetting.DoNotSellList.Contains(itemName))
                {
                    WTLogger.Log($"Removing {itemName} from DoNotSell list");
                    wManagerSetting.CurrentSetting.DoNotSellList.Remove(itemName);
                    settingsChanged = true;
                }
            }

            if (settingsChanged)
            {
                wManagerSetting.CurrentSetting.Save();
            }
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
                // Camp Mojache
                AddBlacklistZone(new Vector3(-4386.497, 178.2069, 26.19132), 160, ContinentId.Kalimdor);
                // ?
                AddBlacklistZone(new Vector3(-956.664, -3754.71, 5.33239), 160, ContinentId.Kalimdor);
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
            // Mudsprocket inn
            OffMeshConnection mudSprocketInn = new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-4587.525, -3172.762, 34.12373, "None"),
                new Vector3(-4598.708, -3173.359, 38.11667, "None"),
                new Vector3(-4609.436, -3172.845, 37.47055, "None"),
                new Vector3(-4621.323, -3172.595, 34.81491, "None")
            }, (int)ContinentId.Kalimdor, OffMeshConnectionType.Bidirectional, true);
            mudSprocketInn.Name = "Mudsprocket Inn";
            OffMeshConnections.Add(mudSprocketInn);

            // Ratchet northern house
            OffMeshConnection ratchetNorthernHouse = new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-859.9623, -3757.758, 19.88704, "None"),
                new Vector3(-847.8208, -3740.918, 22.26572, "None")
            }, (int)ContinentId.Kalimdor, OffMeshConnectionType.Bidirectional, true);
            ratchetNorthernHouse.Name = "Ratchet northern house";
            OffMeshConnections.Add(ratchetNorthernHouse);

            // Sporeggar house
            OffMeshConnection sporeggarHouse = new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(206.3829, 8499.533, 24.57104, "None"),
                new Vector3(194.8526, 8489.873, 27.46523, "None")
            }, (int)ContinentId.Expansion01, OffMeshConnectionType.Bidirectional, true);
            sporeggarHouse.Name = "Sporeggar house";
            OffMeshConnections.Add(sporeggarHouse);

            // Booty bay water
            OffMeshConnection bootyBayWater = new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-14418.96, 467.1775, -1.693841, "Swimming"),
                new Vector3(-14428.78, 461.7385, -1.720309, "Swimming"),
                new Vector3(-14435.47, 457.4826, 1.922136, "None"),
                new Vector3(-14440.56, 453.897, 3.760475, "None")

            }, (int)ContinentId.Azeroth, OffMeshConnectionType.Bidirectional, true);
            bootyBayWater.Name = "Booty Bay water dock";
            OffMeshConnections.Add(bootyBayWater);

            // Booty Bay tunnel
            OffMeshConnection bootyBayTunnel = new OffMeshConnection(new List<Vector3>()
            {
                new Vector3(-14319.32, 444.4599, 23.03997, "None"),
                new Vector3(-14287.69, 431.7805, 33.14453, "None"),
                new Vector3(-14274.26, 415.7346, 37.013, "None"),
                new Vector3(-14279.38, 371.9503, 34.11408, "None"),
                new Vector3(-14269.13, 350.0408, 32.38533, "None"),
                new Vector3(-14241.96, 324.4074, 24.80441, "None")
            }, (int)ContinentId.Azeroth, OffMeshConnectionType.Bidirectional, true);
            bootyBayTunnel.Name = "Booty Bay tunnel";
            OffMeshConnections.Add(bootyBayTunnel);

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
