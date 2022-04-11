using robotManager.Helpful;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using static wManager.Wow.Helpers.PathFinder;

namespace WholesomeToolbox
{
    /// <summary>
    /// Transport related methods (boats, tram, zeps..)
    /// </summary>
    public class WTTransport
    {
        /// <summary>
        /// Adds a transport offmesh
        /// </summary>
        /// <param name="waitForTransport"></param>
        /// <param name="stepIn"></param>
        /// <param name="objectDeparture"></param>
        /// <param name="objectArrival"></param>
        /// <param name="stepOut"></param>
        /// <param name="objectId"></param>
        /// <param name="continentId"></param>
        /// <param name="name"></param>
        /// <param name="precision"></param>
        public static void AddTransportOffMesh(
            Vector3 waitForTransport,
            Vector3 stepIn,
            Vector3 objectDeparture,
            Vector3 objectArrival,
            Vector3 stepOut,
            int objectId,
            ContinentId continentId,
            string name = "",
            float precision = 0.5f)
        {
            OffMeshConnection offMeshConnection = new OffMeshConnection(new List<Vector3>
            {
                waitForTransport,
                new Vector3(stepIn.X, stepIn.Y, stepIn.Z, "None")
                {
                    Action = "c#: Logging.WriteNavigator(\"Waiting for transport\"); " +
                        "if (ObjectManager.Me.InCombatFlagOnly) wManager.Wow.Bot.Tasks.MountTask.DismountMount();" +
                        "while (Conditions.InGameAndConnectedAndProductStartedNotInPause && !ObjectManager.Me.InCombatFlagOnly) " +
                        "{ " +
                            $"var elevator = ObjectManager.GetWoWGameObjectByEntry({objectId}).OrderBy(o => o.GetDistance).FirstOrDefault(); " +
                            $"if (elevator != null && elevator.IsValid && elevator.Position.DistanceTo(new Vector3({objectDeparture.X.ToString().Replace(",", ".")}, {objectDeparture.Y.ToString().Replace(",", ".")}, {objectDeparture.Z.ToString().Replace(",", ".")})) < {precision.ToString().Replace(",", ".")}) " +
                                "break; " +
                            "Thread.Sleep(100); " +
                        "}"
                },
                new Vector3(stepOut.X, stepOut.Y, stepOut.Z, "None")
                {
                    Action = "c#: Logging.WriteNavigator(\"Wait to leave Elevator\"); " +
                        "while (Conditions.InGameAndConnectedAndProductStartedNotInPause) " +
                        "{ " +
                            $"var elevator = ObjectManager.GetWoWGameObjectByEntry({objectId}).OrderBy(o => o.GetDistance).FirstOrDefault(); " +
                            $"if (elevator != null && elevator.IsValid && elevator.Position.DistanceTo(new Vector3({objectArrival.X.ToString().Replace(",", ".")}, {objectArrival.Y.ToString().Replace(",", ".")}, {objectArrival.Z.ToString().Replace(",", ".")})) < {precision.ToString().Replace(",", ".")}) " +
                                "break; " +
                            "Thread.Sleep(100); " +
                        "}"
                },
            }, (int)continentId, OffMeshConnectionType.Unidirectional, false);
            offMeshConnection.Name = name;
            OffMeshConnections.Add(offMeshConnection, true);
        }

        /// <summary>
        /// Waits for a transport to be under specified distance from the player
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="distance"></param>
        public static void WaitForTransport(int transportId, int distance)
        {
            // If the zep is already here, we skip
            if (ObjectManager.GetWoWGameObjectByEntry(transportId).Count > 0 &&
                ObjectManager.GetWoWGameObjectByEntry(transportId).OrderBy(o => o.GetDistance).FirstOrDefault().GetDistance2D <= distance)
                return;

            Logger.Log("Waiting for transport...");
            // Wait for zep
            while (ObjectManager.GetWoWGameObjectByEntry(transportId).Count <= 0 ||
                ObjectManager.GetWoWGameObjectByEntry(transportId).Count > 0 &&
                ObjectManager.GetWoWGameObjectByEntry(transportId).OrderBy(o => o.GetDistance).FirstOrDefault().GetDistance2D > distance)
            {
                if (ObjectManager.GetWoWGameObjectByEntry(transportId).Count <= 0)
                    Logger.Log("The transport is not on in sight yet.");
                else
                    Logger.Log(ObjectManager.GetWoWGameObjectByEntry(transportId).OrderBy(o => o.GetDistance).FirstOrDefault().GetDistance2D.ToString());
                Thread.Sleep(5000);
            }

            // Wait 5 sec before hopping in
            if (ObjectManager.GetWoWGameObjectByEntry(transportId).OrderBy(o => o.GetDistance).FirstOrDefault().GetDistance2D <= distance)
            {
                Thread.Sleep(5000);
            }
        }

        /// <summary>
        ///  Wait on transport (Zepp, elevator etc..) and moves to arrival point when destination reached
        /// </summary>
        /// <param name="arrivalPoint"></param>
        /// <param name="distance"></param>
        public static void WaitOnTransport(Vector3 arrivalPoint, int distance)
        {
            if (ObjectManager.Me.InTransport)
            {
                Logger.Log("Waiting inside transport...");
                while (ObjectManager.Me.Position.DistanceTo(arrivalPoint) > distance)
                {
                    Thread.Sleep(5000);
                }
                Thread.Sleep(5000);
                ForceMoveTo(arrivalPoint);
            }
            else
                Logger.Log("ERROR : Not on transport");
        }

        /// <summary>
        /// Forces a move to a point. Will ignore pathfinder obstacles. Only for safe short distances.
        /// </summary>
        /// <param name="destination"></param>
        public static void ForceMoveTo(Vector3 destination)
        {
            MovementManager.MoveTo(destination);
            while (MovementManager.InMoveTo)
                Thread.Sleep(20);
        }

        /// <summary>
        /// Add recommended transport offmeshes. Contains most WotlK elevators.
        /// </summary>
        public static void AddRecommendedTransportsOffmeshes()
        {
            AddTransportOffMesh(new Vector3(695.7321, -3822.025, 254.6207, "None"), // wait for transport
                new Vector3(704.0106, -3822.148, 254.8952, "None"), // Step in
                new Vector3(700.767, -3823.5, 268.267, "None"), // Object departure
                new Vector3(617.7081, -2890.286, 56.26012, "None"), // Object arrival
                new Vector3(610.707, -2890.53, 42.3438, "None"), // Step out
                190587,
                ContinentId.Northrend,
                "Kamagua gondola TO");

            AddTransportOffMesh(new Vector3(600.0642, -2891.163, 42.33836, "None"), // wait for transport
                new Vector3(592.8513, -2891.575, 42.713, "None"), // Step in
                new Vector3(595.1278, -2892.089, 56.1194, "None"), // Object departure
                new Vector3(678.7067, -3823.943, 268.0588, "None"), // Object arrival
                new Vector3(684.781, -3822.589, 254.6747, "None"), // Step out
                188360,
                ContinentId.Northrend,
                "Kamagua gondola FROM");

            AddTransportOffMesh(new Vector3(1697.43, -5838.462, 11.99705, "None"), // wait for transport
                new Vector3(1690.088, -5831.97, 12.06873, "None"), // Step in
                new Vector3(1680.11, -5824.42, -72.76543), // Object departure
                new Vector3(1680.11, -5824.42, 161.673, "None"), // Object arrival
                new Vector3(1676.99, -5820.689, 248.3792, "None"), // Step out
                190118,
                ContinentId.Northrend,
                "Vengeance Lift UP");

            AddTransportOffMesh(new Vector3(1676.669, -5821.517, 248.3307, "None"), // wait for transport
                new Vector3(1688.307, -5832.458, 246.5121, "None"), // Step in
                new Vector3(1680.11, -5824.42, 161.673, "None"), // Object departure
                new Vector3(1680.11, -5824.42, -72.76543), // Object arrival
                new Vector3(1697.43, -5838.462, 11.99705, "None"), // Step out
                190118,
                ContinentId.Northrend,
                "Vengeance Lift DOWN");

            AddTransportOffMesh(new Vector3(2865.628, 6211.75, 104.262), // wait for transport
                new Vector3(2878.712, 6224.032, 105.3798), // Step in
                new Vector3(2878.315, 6223.635, 105.3792), // Object departure
                new Vector3(2892.18, 6236.34, 208.908), // Object arrival
                new Vector3(2880.497, 6226.416, 208.7462, "None"), // Step out
                188521,
                ContinentId.Northrend,
                "Warsong Hold Elevator UP");

            AddTransportOffMesh(new Vector3(2880.497, 6226.416, 208.7462, "None"), // wait for transport
                new Vector3(2891.717, 6236.516, 208.9086, "None"), // Step in
                new Vector3(2892.18, 6236.34, 208.908), // Object departure
                new Vector3(2878.315, 6223.635, 105.3792), // Object arrival
                new Vector3(2865.628, 6211.75, 104.262), // Step out
                188521,
                ContinentId.Northrend,
                "Warsong Hold Elevator DOWN");

            AddTransportOffMesh(new Vector3(4219.52, 3126.461, 184.3423, "None"), // wait for transport
                new Vector3(4208.915, 3111.077, 184.3453, "None"), // Step in
                new Vector3(4208.69, 3111.24, 183.8219), // Object departure
                new Vector3(4208.69, 3111.24, 335.2971), // Object arrival
                new Vector3(4196.539, 3095.831, 335.8202, "None"), // Step out
                184330,
                ContinentId.Expansion01,
                "Stormspire elevator UP");

            AddTransportOffMesh(new Vector3(4197.577, 3095.454, 335.8203, "None"), // wait for transport
                new Vector3(4209.05, 3111.383, 335.8167, "None"), // Step in
                new Vector3(4208.69, 3111.24, 335.2971), // Object departure
                new Vector3(4208.69, 3111.24, 183.8219), // Object arrival
                new Vector3(4219.52, 3126.461, 184.3423, "None"), // Step out
                184330,
                ContinentId.Expansion01,
                "Stormspire elevator DOWN");

            AddTransportOffMesh(new Vector3(284.8249, 5934.093, 26.58717, "None"), // wait for transport
                new Vector3(285.6157, 5920.79, 26.16297, "None"), // Step in
                new Vector3(285.749, 5918.21, 26.1411, "None"), // Object departure
                new Vector3(283.5827, 5935.404, 149.3924, "None"), // Object arrival
                new Vector3(281.268, 5949.478, 149.8112, "None"), // Step out
                183177,
                ContinentId.Expansion01,
                "Telredor elevator UP");

            AddTransportOffMesh(new Vector3(281.268, 5949.478, 149.8112, "None"), // wait for transport
                new Vector3(283.8581, 5936.767, 149.417, "None"), // Step in
                new Vector3(283.5827, 5935.404, 149.3924, "None"), // Object departure
                new Vector3(285.749, 5918.21, 26.1411, "None"), // Object arrival
                new Vector3(284.8249, 5934.093, 26.58717, "None"), // Step out
                183177,
                ContinentId.Expansion01,
                "Telredor elevator DOWN");

            AddTransportOffMesh(new Vector3(-2009.204, 5478.759, 3.718776, "None"), // wait for transport
                new Vector3(-2018.58, 5481.813, 3.814111, "None"), // Step in
                new Vector3(-2021.46, 5482.74, 3.004047, "None"), // Object departure
                new Vector3(-2021.46, 5482.74, 53.70876, "None"), // Object arrival
                new Vector3(-2032.718, 5486.205, 54.53993, "None"), // Step out
                183203,
                ContinentId.Expansion01,
                "Scryers elevator UP");

            AddTransportOffMesh(new Vector3(-2032.718, 5486.205, 54.53993, "None"), // wait for transport
                new Vector3(-2022.939, 5483.274, 54.52467, "None"), // Step in
                new Vector3(-2021.46, 5482.74, 53.70876, "None"), // Object departure
                new Vector3(-2021.46, 5482.74, 3.004047, "None"), // Object arrival
                new Vector3(-2009.204, 5478.759, 3.718776, "None"), // Step out
                183203,
                ContinentId.Expansion01,
                "Scryers elevator DOWN");

            AddTransportOffMesh(new Vector3(1595.485, 202.7518, 55.24096, "None"), // wait for transport
                new Vector3(1595.344, 190.6781, 55.25394, "None"), // Step in
                new Vector3(1595.26, 188.644, 55.71788, "None"), // Object departure
                new Vector3(1595.26, 188.644, -40.7835, "None"), // Object arrival
                new Vector3(1595.197, 173.3713, -41.4236, "None"), // Step out
                20652,
                ContinentId.Azeroth,
                "Undercity elevator DOWN");

            AddTransportOffMesh(new Vector3(1595.197, 173.3713, -41.4236, "None"), // wait for transport
                new Vector3(1595.146, 187.4255, -41.24735, "None"), // Step in
                new Vector3(1595.26, 188.644, -40.7835, "None"), // Object departure
                new Vector3(1595.26, 188.644, 55.71788, "None"), // Object arrival
                new Vector3(1595.485, 202.7518, 55.24096, "None"), // Step out
                20652,
                ContinentId.Azeroth,
                "Undercity elevator UP");

            OffMeshConnections.Save();
        }
    }
}
