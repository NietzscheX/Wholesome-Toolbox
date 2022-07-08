using robotManager.Helpful;
using System.Threading;
using wManager.Wow.Bot.Tasks;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;
using Timer = robotManager.Helpful.Timer;

namespace WholesomeToolbox
{
    /// <summary>
    /// Methods to travel to and through continents
    /// </summary>
    public class WTTravel
    {
        // Tramway
        readonly static int deeprunTramStormwindToIronforgeId = 176082;
        readonly static Vector3 bayTramStormwindToIronforge = new Vector3(-34.445, 2492.806, -4.289737, "None");
        readonly static Vector3 insideTramStormwindToIronforge = new Vector3(-44.94446, 2492.615, -3.47941, "None");
        readonly static Vector3 bayTramStormwindToIronforgeArrival = new Vector3(-27.32375, 9.688682, -4.296832, "None");

        readonly static int deeprunTramIronforgeToStomwindId = 176081;
        readonly static Vector3 bayTramIronforgeToStormwind = new Vector3(16.87345, 8.440852, -4.29735, "None");
        readonly static Vector3 insideTramIronforgeToStormwind = new Vector3(4.707247, 8.58511, -3.456941, "None");
        readonly static Vector3 bayTramIronforgeToStormwindArrival = new Vector3(18.3296, 2490.827, -4.291731, "None");

        // Zeppelins
        readonly static int zeppelinTirisfalToOrgrimmarId = 164871;
        readonly static int zeppelinKalidmdorToBoreanTundraId = 186238;
        readonly static int zeppelinTirisfalToStranglethornId = 176495;
        readonly static int zeppelinTirisfalToHowlingFjord = 181689;

        readonly static Vector3 howlingFjordPlatformZepTirisfal = new Vector3(1974.928, -6099.246, 67.15016, "None");
        readonly static Vector3 insideZeppelinHowlingFjordToTirisfal = new Vector3(1973.273, -6100.806, 67.15335, "None");

        readonly static Vector3 tirisfalPlatformHowlingFjord = new Vector3(2062.986, 356.4112, 82.45396, "None");
        readonly static Vector3 insideZeppelinTirisfalToHowlingFjord = new Vector3(2060.194, 372.4912, 82.45258, "None");

        readonly static Vector3 tirisfalPlatformZepStranglethorn = new Vector3(2057.805, 242.4949, 99.76875, "None");
        readonly static Vector3 insideZeppelinTirisfalToStranglethorn = new Vector3(2056.706, 231.9014, 100.0376, "None");

        readonly static Vector3 stranglethornPlatformZepTirisfal = new Vector3(-12415.1, 207.7803, 31.49846, "None");
        readonly static Vector3 insideZeppelinStranglethornToTirisfal = new Vector3(-12401.24, 206.9413, 32.03218, "None");

        readonly static Vector3 tirisfalPlatformZepOrgrimmar = new Vector3(2066.407, 285.9733, 97.03147);
        readonly static Vector3 insideZeppelinTirisfalToOrgrimmar = new Vector3(2068.5, 296.4182, 97.28456);

        readonly static Vector3 orgrimmarPlatformZepTirisfal = new Vector3(1324.798, -4652.622, 53.78583);
        readonly static Vector3 insideZeppelinKalimdorToTirisfal = new Vector3(1312.742, -4653.958, 54.14745);

        readonly static Vector3 tirisfalPlatformZepNorthrend = new Vector3(2061.558, 359.2196, 83.47895);
        readonly static Vector3 insideZeppelinTirisfalToNorthrend = new Vector3(2059.505, 374.1549, 82.44972);

        readonly static Vector3 northrendPlatformZepTirisfal = new Vector3(1973.989, -6099.464, 67.15666);
        readonly static Vector3 insideZeppelinNorthrendToTirisfal = new Vector3(1984.397, -6089.137, 67.68417);

        readonly static Vector3 kalimdorlPlatformZepBoreanTundra = new Vector3(1179.33, -4150.091, 52.13512);
        readonly static Vector3 insideZeppelinKalimdorToBoreanTundra = new Vector3(1192.992, -4142.117, 52.73592);

        readonly static Vector3 boreanTundraPlatformZepKalimdor = new Vector3(2829.167, 6178.443, 121.9824f);
        readonly static Vector3 insideZeppelinNorthrendToKalimdor = new Vector3(2844.347, 6192.584, 122.2752);

        //Ships
        readonly static Vector3 bayRatchetToBootyBay = new Vector3(-993.4946, -3825.295, 5.689055, "None");
        readonly static Vector3 insideShipRatchetToBootyBay = new Vector3(-998.897, -3835.783, 6.215654, "None");

        readonly static Vector3 bayBootyBayToRatchet = new Vector3(-14285.3, 556.2886, 8.837771, "None");
        readonly static Vector3 insideShipBootyBayToRatchet = new Vector3(-14276.39, 575.9086, 6.084363, "None");

        readonly static Vector3 bayAzuremystToDarkshore = new Vector3(-4263.875, -11335.03, 5.859486, "None");
        readonly static Vector3 insideShipAzuremystToDarkshore = new Vector3(-4262.877, -11324.02, 4.91574, "None");

        readonly static Vector3 bayDarkshoreToAzuremyst = new Vector3(6542.727, 922.8457, 5.912864, "None");
        readonly static Vector3 insideShipDarkshoreToAzuremyst = new Vector3(6550.912, 945.3401, 4.898618, "None");

        readonly static Vector3 bayDarkshoreToDarnassus = new Vector3(6576.859, 769.8532, 5.588161, "None");
        readonly static Vector3 insideShipDarkshoreToDarnassus = new Vector3(6587.057, 767.6591, 5.10977, "None");

        readonly static Vector3 bayDarnassusToDarkshore = new Vector3(8553.19, 1020.2, 5.543461, "None");
        readonly static Vector3 insideShipDarnassusToDarkshore = new Vector3(8541.434, 1021.083, 5.085521, "None");

        readonly static Vector3 bayDarkshoreToStormwind = new Vector3(6424.98, 818.6711, 5.490422, "None");
        readonly static Vector3 insideShipDarkshoreToStormwind = new Vector3(6414.219, 822.085, 6.1804, "None");

        readonly static Vector3 bayStormwindToDarkshore = new Vector3(-8640.113, 1327.584, 5.233279, "None");
        readonly static Vector3 insideShipStormwindToDarkshore = new Vector3(-8646.059, 1339.308, 6.215415, "None");

        readonly static Vector3 bayStormwindToBoreanTundra = new Vector3(-8295.06, 1408.721, 4.688217, "None");
        readonly static Vector3 insideShipStormwindToBoreanTundra = new Vector3(-8291.178, 1427.616, 9.473845, "None");

        readonly static Vector3 bayBoreanTundraToStormwind = new Vector3(2231.234, 5135.821, 5.343364, "None");
        readonly static Vector3 insideShipBoreanTundraToStormwind = new Vector3(2232.324, 5114.973, 9.400736, "None");

        readonly static Vector3 bayDustwallowToMenethil = new Vector3(-4000.404, -4724.158, 4.876398, "None");
        readonly static Vector3 insideShipDustwallowToMenethil = new Vector3(-4010.497, -4741.962, 6.17096, "None");

        readonly static Vector3 bayMenethilToDustwallow = new Vector3(-3893.388, -602.8146, 5.425149, "None");
        readonly static Vector3 insideShipMenethilToDustwallow = new Vector3(-3904.25, -577.7352, 6.059737, "None");


        readonly static Vector3 bayMenethilToHowlingFjord = new Vector3(-3724.361, -583.2341, 4.74352, "None");
        readonly static Vector3 insideShipMenethilToHowlingFjord = new Vector3(-3711.364, -573.7974, 9.489109, "None");

        readonly static Vector3 bayHowlingFjordToMenethil = new Vector3(591.8311, -5099.395, 5.260396, "None");
        readonly static Vector3 insideShipHowlingFjordToMenethil = new Vector3(588.0685, -5120.662, 9.447546, "None");


        readonly static int shipAzuremystToDarkshoreId = 181646;
        readonly static int shipMenethilToHowlingFjord = 181688;
        readonly static int shipDarkshoreToStormwindId = 176310;
        readonly static int shipStormwindToBoreanTundraId = 190536;
        readonly static int shipDarkshoreToDarnassusId = 176244;
        readonly static int shipRatchetToBootyBayId = 20808;
        readonly static int shipMenethilToDustwallowId = 176231;


        // Portals
        readonly static int oGPortalToBlastedLandsId = 195142;

        readonly static Vector3 oGPortalToBlastedLandsPosition = new Vector3(1472.55f, -4215.7f, 59.221f);

        readonly static int silverMoonPortalToTirisfal = 184502;
        readonly static Vector3 silverMoonPortalToTirisfalPosition = new Vector3(10034.58, -6999.497, 61.59146);

        /*readonly static int ExodarPortalToBlastedLandsId = 195141;
        readonly static Vector3 ExodarPortalToBlastedLandsPosition = new Vector3(-4037.81, -11555.6, -138.324f);

        readonly static int DarnassusPortalToBlastedlandsId = 195141;
        readonly static Vector3 DarnassusPortalToBlastedlandsPosition = new Vector3(9661.83, 2509.61, 1331.63f);*/

        readonly static int stormwindPortalToBlastedlandsId = 195141;
        readonly static Vector3 stormwindPortalToBlastedlandsPosition = new Vector3(-9007.58, 871.87, 129.692f);

        //  Shattrath portals
        readonly static int shattrathPortalToKalimdorId = 183323;
        readonly static Vector3 shattrathPortalToOrgrimmarPosition = new Vector3(-1934.205f, 5452.766f, -12.42705f);

        /*readonly static int shattrathPortalToThunderbluffId = 183326;
        readonly static Vector3 shattrathPortalToThunderbluffPosition = new Vector3(-1936.32, 5445.95, -12.4282f);

        readonly static int shattrathPortalToUndercityId = 183327;
        readonly static Vector3 shattrathPortalToUndercityPosition = new Vector3(-1931.48, 5460.49, -12.4281f);

        readonly static int shattrathPortalTQueldanasId = 187056;
        readonly static Vector3 shattrathPortalToQueldanasPosition = new Vector3(-1839.88, 5500.6, -12.4279f);

        readonly static int shattrathPortalToDarnassusId = 183317;
        readonly static Vector3 shattrathPortalToDarnassusPosition = new Vector3(-1790.98, 5413.98, -12.4282f);
        */
        readonly static int shattrathPortalToStormwindId = 183325;
        readonly static Vector3 shattrathPortalToStormwindPosition = new Vector3(-1792.78, 5406.54, -12.4279f);

        readonly static int shattrathPortalToIronforgeId = 183322;
        readonly static Vector3 shattrathPortalToIronforgePosition = new Vector3(-1795.79, 5399.63, -12.4281f);
        /*
        readonly static int ShattrathPortalToExodarId = 183321;
        readonly static Vector3 ShattrathPortalToExodarPosition = new Vector3(-1880.28, 5357.53, -12.4281f);

        readonly static int ShattrathPortalToSilvermoonId = 183324;
        readonly static Vector3 ShattrathPortalToSilvermoonPosition = new Vector3(-1894.69, 5362.34, -12.4282f);*/


        // Dalaran portals
        /*readonly static int DalaranPortalToStormwindId = 190960;
        readonly static Vector3 DalaranPortalToStormwindPosition = new Vector3(5719.19, 719.681, 641.728f);

        readonly static int DalaranPortalToIronforgeId = 191008;
        readonly static Vector3 DalaranPortalToIronforgePosition = new Vector3(5712.68, 724.845, 641.736f);

        readonly static int DalaranPortalToDarnassusId = 191006;
        readonly static Vector3 DalaranPortalToDarnassusPosition = new Vector3(5706.16, 730.102, 641.745f);

        readonly static int DalaranPortalToExodarId = 191007;
        readonly static Vector3 DalaranPortalToExodarPosition = new Vector3(5699.58, 735.469, 641.769f);*/

        readonly static int allianceDalaranPortalToShattrathId = 191013;
        readonly static Vector3 allianceDalaranPortalToShattrathPosition = new Vector3(5697.49, 744.912, 641.819f);

        readonly static int dalaranPortalToUndercityId = 191012;
        readonly static Vector3 dalaranPortalToUndercityPosition = new Vector3(5934.66, 590.688, 640.575, "Flying");

        readonly static int dalaranPortalToOGId = 191009;
        readonly static Vector3 dalaranPortalToOGPosition = new Vector3(5925.85, 593.25, 640.563, "Flying");

        readonly static int dalaranPortalToShattrathId = 191014;
        readonly static Vector3 dalaranPortalToShattrathPosition = new Vector3(5941.66, 584.887, 640.574, "Flying");

        // ********** FROM EK **********

        /// <summary>
        /// Take tramway from Ironforge to Stormwind (must be in Deeprun tram area)
        /// </summary>
        public static void TakeTramFromIronforgeToStormwind()
        {
            WTLogger.Log($"Hopping on tramway to Stormwind");
            GoToTask.ToPosition(bayTramIronforgeToStormwind);
            if (ObjectManager.Me.Position.DistanceTo(bayTramIronforgeToStormwind) < 4)
            {
                WTTransport.WaitForTransport(deeprunTramIronforgeToStomwindId, 20);
                WTTransport.ForceMoveTo(insideTramIronforgeToStormwind);
                WTTransport.WaitOnTransport(bayTramIronforgeToStormwindArrival, 30);
            }
        }

        /// <summary>
        /// Exits Deeprun Tram to Stormwind
        /// </summary>
        public static void ExitDeeprunTramToStormwind()
        {
            WTLogger.Log($"Exiting Deeprun Tram to Stormwind");
            Vector3 exitDRTram = new Vector3(64.14916, 2491.184, -4.29601, "None");
            Vector3 behindPortalDRtram = new Vector3(84.14916, 2491.184, -4.29601, "None");
            if (ObjectManager.Me.Position.DistanceTo(exitDRTram) > 5)
            {
                GoToTask.ToPosition(exitDRTram);
            }
            else
            {
                Timer timer = new Timer(5000);
                MovementManager.MoveTo(behindPortalDRtram);
                while (!timer.IsReady && WTLocation.GetMinimapZoneText == "Deeprun Tram")
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Enters Deeprun tram from Ironforge
        /// </summary>
        public static void EnterIronForgedDeeprunTram()
        {
            WTLogger.Log($"Taking tramway to Stormwind");
            Vector3 entranceDRTram = new Vector3(-4838.456, -1316.167, 501.8683);
            Vector3 behindPortalDRtram = new Vector3(-4838.456, -1340.167, 501.8683);
            if (ObjectManager.Me.Position.DistanceTo(entranceDRTram) > 5)
            {
                GoToTask.ToPosition(entranceDRTram);
            }
            else
            {
                Timer timer = new Timer(5000);
                MovementManager.MoveTo(behindPortalDRtram);
                while (!timer.IsReady && WTLocation.GetMinimapZoneText != "Deeprun Tram")
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Exits Deeprun Tram to Ironforge
        /// </summary>
        public static void ExitDeeprunTramToIronforge()
        {
            WTLogger.Log($"Exiting Deeprun Tram to Ironforge");
            Vector3 exitDRTram = new Vector3(66.81684, 9.562094, -4.297355, "None");
            Vector3 behindPortalDRtram = new Vector3(88.81684, 9.562094, -4.297355, "None");
            if (ObjectManager.Me.Position.DistanceTo(exitDRTram) > 5)
            {
                GoToTask.ToPosition(exitDRTram);
            }
            else
            {
                Timer timer = new Timer(5000);
                MovementManager.MoveTo(behindPortalDRtram);
                while (!timer.IsReady && WTLocation.GetMinimapZoneText == "Deeprun Tram")
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Takes tramway to Ironforge (must be in Deeprun Tram area)
        /// </summary>
        public static void TakeTramFromStormwindToIronforge()
        {
            WTLogger.Log($"Hopping on tramway to Ironforge");
            GoToTask.ToPosition(bayTramStormwindToIronforge);
            if (ObjectManager.Me.Position.DistanceTo(bayTramStormwindToIronforge) < 4)
            {
                WTTransport.WaitForTransport(deeprunTramStormwindToIronforgeId, 15);
                WTTransport.ForceMoveTo(insideTramStormwindToIronforge);
                WTTransport.WaitOnTransport(bayTramStormwindToIronforgeArrival, 30);
            }
        }

        /// <summary>
        /// Enters Deeprun Tram from Stormwind
        /// </summary>
        public static void EnterStormwindDeeprunTram()
        {
            WTLogger.Log($"Taking tramway to Ironforge");
            Vector3 entranceDRTram = new Vector3(-8355.539, 525.3418, 91.79753);
            Vector3 behindPortalDRtram = new Vector3(-8330, 500.3350, 91.79753);
            if (ObjectManager.Me.Position.DistanceTo(entranceDRTram) > 5)
            {
                GoToTask.ToPosition(entranceDRTram);
            }
            else
            {
                Timer timer = new Timer(5000);
                MovementManager.MoveTo(behindPortalDRtram);
                while (!timer.IsReady && WTLocation.GetMinimapZoneText != "Deeprun Tram")
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Take portal to Outlands
        /// </summary>
        public static void PortalBlastedLandsToOutlands()
        {
            WTLogger.Log($"Traversing portal to Outlands");
            GoToTask.ToPosition(new Vector3(-11920.39, -3206.81, -15.35475f));
            Thread.Sleep(5000);
            GoToTask.ToPosition(new Vector3(-182.5485, 1023.459, 54.23014));
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take zeppelin from Tirisfal to Orgrimmar
        /// </summary>
        public static void ZeppelinTirisfalToOrgrimmar()
        {
            WTLogger.Log($"Taking zeppelin to Orgrimmar");
            GoToTask.ToPosition(tirisfalPlatformZepOrgrimmar);
            if (ObjectManager.Me.Position.DistanceTo(tirisfalPlatformZepOrgrimmar) < 4)
            {
                WTTransport.WaitForTransport(zeppelinTirisfalToOrgrimmarId, 15);
                WTTransport.ForceMoveTo(insideZeppelinTirisfalToOrgrimmar);
                WTTransport.WaitOnTransport(orgrimmarPlatformZepTirisfal, 15);
            }
        }

        /// <summary>
        /// Take zeppelin from Tirisfal To Stranglethorn vale
        /// </summary>
        public static void ZeppelingTirisfalToStrangelthorn()
        {
            WTLogger.Log("Taking zeppelin to Stranglethorn");
            GoToTask.ToPosition(tirisfalPlatformZepStranglethorn);
            if (ObjectManager.Me.Position.DistanceTo(tirisfalPlatformZepStranglethorn) < 4)
            {
                WTTransport.WaitForTransport(zeppelinTirisfalToStranglethornId, 15);
                WTTransport.ForceMoveTo(insideZeppelinTirisfalToStranglethorn);
                WTTransport.WaitOnTransport(stranglethornPlatformZepTirisfal, 15);
            }
        }

        /// <summary>
        /// Take zeppelin from Stranglethorn vale to Tirisfal
        /// </summary>
        public static void ZeppelingStrangelthornToTirisfal()
        {
            WTLogger.Log("Taking zeppelin to Tirisfal");
            GoToTask.ToPosition(stranglethornPlatformZepTirisfal);
            if (ObjectManager.Me.Position.DistanceTo(stranglethornPlatformZepTirisfal) < 4)
            {
                WTTransport.WaitForTransport(zeppelinTirisfalToStranglethornId, 15);
                WTTransport.ForceMoveTo(insideZeppelinStranglethornToTirisfal);
                WTTransport.WaitOnTransport(tirisfalPlatformZepStranglethorn, 15);
            }
        }

        /// <summary>
        /// Take boat from Booty Bay to Ratchet
        /// </summary>
        public static void ShipBootyBayToRatchet()
        {
            WTLogger.Log("Taking ship to Ratchet");
            if (ObjectManager.Me.Position.X > -14260)
            {
                // Make sure we don't swim to docks
                GoToTask.ToPosition(new Vector3(-14268.2, 344.7384, 31.02225, "None"));
                return;
            }
            GoToTask.ToPosition(bayBootyBayToRatchet);
            if (ObjectManager.Me.Position.DistanceTo(bayBootyBayToRatchet) < 4)
            {
                WTTransport.WaitForTransport(shipRatchetToBootyBayId, 50);
                WTTransport.ForceMoveTo(insideShipBootyBayToRatchet);
                WTTransport.WaitOnTransport(bayRatchetToBootyBay, 30);
            }
        }

        /// <summary>
        /// Take boat from Stormwond to Darkshore
        /// </summary>
        public static void ShipStormwindToDarkshore()
        {
            WTLogger.Log("Taking ship to Darkshore");
            GoToTask.ToPosition(bayStormwindToDarkshore);
            if (ObjectManager.Me.Position.DistanceTo(bayStormwindToDarkshore) < 4)
            {
                WTTransport.WaitForTransport(shipDarkshoreToStormwindId, 30);
                WTTransport.ForceMoveTo(insideShipStormwindToDarkshore);
                WTTransport.WaitOnTransport(bayDarkshoreToStormwind, 50);
            }
        }

        /// <summary>
        /// Take boat from Menethil Harbor to Dustwallow
        /// </summary>
        public static void ShipMenethilToDustwallow()
        {
            WTLogger.Log("Taking ship to Dustwallow Bay");
            GoToTask.ToPosition(bayMenethilToDustwallow);
            if (ObjectManager.Me.Position.DistanceTo(bayMenethilToDustwallow) < 4)
            {
                WTTransport.WaitForTransport(shipMenethilToDustwallowId, 30);
                WTTransport.ForceMoveTo(insideShipMenethilToDustwallow);
                WTTransport.WaitOnTransport(bayDustwallowToMenethil, 50);
            }
        }

        /// <summary>
        /// Take boat from Azuremyst to Darkshore
        /// </summary>
        public static void ShipAzuremystToDarkshore()
        {
            WTLogger.Log("Taking ship to Darkshore");
            GoToTask.ToPosition(bayAzuremystToDarkshore);
            if (ObjectManager.Me.Position.DistanceTo(bayAzuremystToDarkshore) < 4)
            {
                WTTransport.WaitForTransport(shipAzuremystToDarkshoreId, 30);
                WTTransport.ForceMoveTo(insideShipAzuremystToDarkshore);
                WTTransport.WaitOnTransport(bayDarkshoreToAzuremyst, 50);
            }
        }

        /// <summary>
        /// Take boat from Darkshore to Azuremyst
        /// </summary>
        public static void ShipDarkshoreToAzuremyst()
        {
            WTLogger.Log("Taking ship to Azuremyst");
            GoToTask.ToPosition(bayDarkshoreToAzuremyst);
            if (ObjectManager.Me.Position.DistanceTo(bayDarkshoreToAzuremyst) < 4)
            {
                WTTransport.WaitForTransport(shipAzuremystToDarkshoreId, 30);
                WTTransport.ForceMoveTo(insideShipDarkshoreToAzuremyst);
                WTTransport.WaitOnTransport(bayAzuremystToDarkshore, 50);
            }
        }

        // ********** FROM OUTLANDS **********

        /// <summary>
        /// Take portal from Shattrah to Stormwind
        /// </summary>
        public static void PortalShattrathToStormwind()
        {
            WTLogger.Log("Taking portal to Stormwind");
            GoToTask.ToPositionAndIntecractWithGameObject(shattrathPortalToStormwindPosition, shattrathPortalToStormwindId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take portal from Shattrah to Ironforge
        /// </summary>
        public static void PortalShattrathToIronforge()
        {
            WTLogger.Log("Taking portal to Ironforge");
            GoToTask.ToPositionAndIntecractWithGameObject(shattrathPortalToIronforgePosition, shattrathPortalToIronforgeId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take portal from Shattrah to Orgrimmar
        /// </summary>
        public static void PortalShattrathToOrgrimmar()
        {
            WTLogger.Log("Taking portal to Orgrimmar");
            GoToTask.ToPositionAndIntecractWithGameObject(shattrathPortalToOrgrimmarPosition, shattrathPortalToKalimdorId);
            Thread.Sleep(5000);
        }

        // ********** FROM KALIMDOR **********

        /// <summary>
        /// Take portal from Rut'Theran village to Darnassus
        /// </summary>
        public static void PortalRutTheranToDarnassus()
        {
            WTLogger.Log("Taking teleporter from Rut'Theran to Darnassus");
            Vector3 prep = new Vector3(8778.752, 969.5687, 30.38474, "None");
            Vector3 portal = new Vector3(8798.752, 969.5687, 30.38474, "None");
            if (ObjectManager.Me.Position.DistanceTo(prep) > 8)
            {
                GoToTask.ToPosition(prep);
            }
            else
            {
                MovementManager.MoveTo(portal);
                Timer timer = new Timer(5000);
                while (MovementManager.InMoveTo && (ObjectManager.Me.Position.Z < 600 || !timer.IsReady))
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Take boat from Darkshore to Rut'Theran village
        /// </summary>
        public static void ShipDarkShoreToRutTheran()
        {
            WTLogger.Log("Taking ship to Rut'Theran");
            GoToTask.ToPosition(bayDarkshoreToDarnassus);
            if (ObjectManager.Me.Position.DistanceTo(bayDarkshoreToDarnassus) < 4)
            {
                WTTransport.WaitForTransport(shipDarkshoreToDarnassusId, 30);
                WTTransport.ForceMoveTo(insideShipDarkshoreToDarnassus);
                WTTransport.WaitOnTransport(bayDarnassusToDarkshore, 50);
            }
        }

        /// <summary>
        /// Take boat from Darkshore to Stormwind
        /// </summary>
        public static void ShipDarkshoreToStormwind()
        {
            WTLogger.Log("Taking ship to Stormwind");
            GoToTask.ToPosition(bayDarkshoreToStormwind);
            if (ObjectManager.Me.Position.DistanceTo(bayDarkshoreToStormwind) < 4)
            {
                WTTransport.WaitForTransport(shipDarkshoreToStormwindId, 30);
                WTTransport.ForceMoveTo(insideShipDarkshoreToStormwind);
                WTTransport.WaitOnTransport(bayStormwindToDarkshore, 50);
            }
        }

        /// <summary>
        /// Take boat from Dustwallow to Menethil Harbor
        /// </summary>
        public static void ShipDustwallowToMenethil()
        {
            WTLogger.Log("Taking ship to Menethil Hardbor");
            GoToTask.ToPosition(bayDustwallowToMenethil);
            if (ObjectManager.Me.Position.DistanceTo(bayDustwallowToMenethil) < 4)
            {
                WTTransport.WaitForTransport(shipMenethilToDustwallowId, 30);
                WTTransport.ForceMoveTo(insideShipDustwallowToMenethil);
                WTTransport.WaitOnTransport(bayMenethilToDustwallow, 50);
            }
        }

        /// <summary>
        /// Take boat from Rut'Theran village to Darkshore
        /// </summary>
        public static void ShipRutTheranToDarkshore()
        {
            WTLogger.Log("Taking ship to Darkshore");
            GoToTask.ToPosition(bayDarnassusToDarkshore);
            if (ObjectManager.Me.Position.DistanceTo(bayDarnassusToDarkshore) < 4)
            {
                WTTransport.WaitForTransport(shipDarkshoreToDarnassusId, 30);
                WTTransport.ForceMoveTo(insideShipDarnassusToDarkshore);
                WTTransport.WaitOnTransport(bayDarkshoreToDarnassus, 50);
            }
        }


        /// <summary>
        /// Take portal from Darnassus to Rut'Theran village
        /// </summary>
        public static void PortalDarnassusToRutTheran()
        {
            WTLogger.Log("Taking teleporter from from Darnassus to Rut'Theran");
            Vector3 prep = new Vector3(9946.391, 2596.067, 1316.194, "None");
            Vector3 portal = new Vector3(9946.391, 2630.067, 1316.194, "None");
            if (ObjectManager.Me.Position.DistanceTo(prep) > 8)
            {
                GoToTask.ToPosition(prep);
            }
            else
            {
                MovementManager.MoveTo(portal);
                Timer timer = new Timer(5000);
                while (MovementManager.InMoveTo && (ObjectManager.Me.Position.Z > 600 || !timer.IsReady))
                    Thread.Sleep(100);
                MovementManager.StopMoveTo();
            }
        }

        /// <summary>
        /// Take portal from Stormwind to Blasted Lands
        /// </summary>
        public static void PortalStormwindToBlastedLands()
        {
            WTLogger.Log("Taking portal to Blasted lands");
            GoToTask.ToPositionAndIntecractWithGameObject(stormwindPortalToBlastedlandsPosition, stormwindPortalToBlastedlandsId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take boat from Ratchet to Booty Bay
        /// </summary>
        public static void ShipRatchetToBootyBay()
        {
            WTLogger.Log("Taking ship to Booty Bay");
            GoToTask.ToPosition(bayRatchetToBootyBay);
            if (ObjectManager.Me.Position.DistanceTo(bayRatchetToBootyBay) < 4)
            {
                WTTransport.WaitForTransport(shipRatchetToBootyBayId, 30);
                WTTransport.ForceMoveTo(insideShipRatchetToBootyBay);
                WTTransport.WaitOnTransport(bayBootyBayToRatchet, 50);
                WTTransport.ForceMoveTo(new Vector3(-14295.3, 535.4561, 8.808624, "None")); // safety for pathfinder
            }
        }

        /// <summary>
        /// Take zeppelin from Orgrimmar to Tirisfal
        /// </summary>
        public static void ZeppelinOrgrimmarToTirisfal()
        {
            WTLogger.Log("Taking zeppelin to Tirisfal");
            GoToTask.ToPosition(orgrimmarPlatformZepTirisfal);
            if (ObjectManager.Me.Position.DistanceTo(orgrimmarPlatformZepTirisfal) < 4)
            {
                WTTransport.WaitForTransport(zeppelinTirisfalToOrgrimmarId, 15);
                WTTransport.ForceMoveTo(insideZeppelinKalimdorToTirisfal);
                WTTransport.WaitOnTransport(tirisfalPlatformZepOrgrimmar, 15);
            }
        }

        /// <summary>
        /// Take portal from Orgrimmar to Blasted Lands
        /// </summary>
        public static void PortalFromOrgrimmarToBlastedLands()
        {
            WTLogger.Log("Taking portal to Blasted Lands");
            GoToTask.ToPositionAndIntecractWithGameObject(oGPortalToBlastedLandsPosition, oGPortalToBlastedLandsId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take portal from Silvermoon City to Tirisfal
        /// </summary>
        public static void PortalFromSilvermoonToTirisfal()
        {
            WTLogger.Log("Taking Orb of Translocation to Tirisfal");
            GoToTask.ToPositionAndIntecractWithGameObject(silverMoonPortalToTirisfalPosition, silverMoonPortalToTirisfal);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take zeppelin from Orgrimmar to Borean Tundra
        /// </summary>
        public static void ZeppelinOrgrimmarToBoreanTundra()
        {
            WTLogger.Log("Taking zeppelin to Borean Tundra");
            GoToTask.ToPosition(kalimdorlPlatformZepBoreanTundra);
            if (ObjectManager.Me.Position.DistanceTo(kalimdorlPlatformZepBoreanTundra) < 4)
            {
                WTTransport.WaitForTransport(zeppelinKalidmdorToBoreanTundraId, 30);
                WTTransport.ForceMoveTo(insideZeppelinKalimdorToBoreanTundra);
                WTTransport.WaitOnTransport(boreanTundraPlatformZepKalimdor, 25);
            }
        }

        /// <summary>
        /// Take zeppelin from Tirisfal to Howling Fjord
        /// </summary>
        public static void ZeppelinTirisfalToHowlingFjord()
        {
            WTLogger.Log("Taking zeppelin to Howling Fjord");
            GoToTask.ToPosition(tirisfalPlatformHowlingFjord);
            if (ObjectManager.Me.Position.DistanceTo(tirisfalPlatformHowlingFjord) < 4)
            {
                WTTransport.WaitForTransport(zeppelinTirisfalToHowlingFjord, 30);
                WTTransport.ForceMoveTo(insideZeppelinTirisfalToHowlingFjord);
                WTTransport.WaitOnTransport(howlingFjordPlatformZepTirisfal, 25);
            }
        }

        /// <summary>
        /// Take boat from Stormwind to Borean Tundra
        /// </summary>
        public static void ShipStormwindToBoreanTundra()
        {
            WTLogger.Log("Taking ship to Borean Tundra");
            GoToTask.ToPosition(bayStormwindToBoreanTundra, 0, true);
            if (ObjectManager.Me.Position.DistanceTo(bayStormwindToBoreanTundra) < 4)
            {
                WTTransport.WaitForTransport(shipStormwindToBoreanTundraId, 30);
                WTTransport.ForceMoveTo(insideShipStormwindToBoreanTundra);
                WTTransport.WaitOnTransport(bayBoreanTundraToStormwind, 25);
            }
        }

        /// <summary>
        /// Take boat from Menethil Harbor to Howling Fjord
        /// </summary>
        public static void ShipMenethilToHowlingFjord()
        {
            WTLogger.Log("Taking ship to Howling Fjord");
            GoToTask.ToPosition(bayMenethilToHowlingFjord, 0, true);
            if (ObjectManager.Me.Position.DistanceTo(bayMenethilToHowlingFjord) < 4)
            {
                WTTransport.WaitForTransport(shipMenethilToHowlingFjord, 30);
                WTTransport.ForceMoveTo(insideShipMenethilToHowlingFjord);
                WTTransport.WaitOnTransport(bayHowlingFjordToMenethil, 25);
            }
        }

        // ********** FROM NORTHREND **********

        /// <summary>
        /// Take boat from Borean Tundra to Stormwind
        /// </summary>
        public static void ShipBoreanTundraToStormwind()
        {
            WTLogger.Log("Taking ship to Stormwind");
            GoToTask.ToPosition(bayBoreanTundraToStormwind, 0, true);
            if (ObjectManager.Me.Position.DistanceTo(bayBoreanTundraToStormwind) < 4)
            {
                WTTransport.WaitForTransport(shipStormwindToBoreanTundraId, 30);
                GoToTask.ToPosition(insideShipBoreanTundraToStormwind, 1);
                WTTransport.WaitOnTransport(bayStormwindToBoreanTundra, 25);
            }
        }

        /// <summary>
        /// Take portal from Dalaran to Undercity
        /// </summary>
        public static void PortalDalaranToUndercity()
        {
            WTLogger.Log("Taking portal to Undercity");
            GoToTask.ToPositionAndIntecractWithGameObject(dalaranPortalToUndercityPosition, dalaranPortalToUndercityId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take portal from Dalaran to Orgrimmar
        /// </summary>
        public static void PortalDalaranToOrgrimmar()
        {
            WTLogger.Log("Taking portal to Orgrimmar");
            GoToTask.ToPositionAndIntecractWithGameObject(dalaranPortalToOGPosition, dalaranPortalToOGId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take alliance portal from Dalaran to Shattrath
        /// </summary>
        public static void AlliacePortalDalaranToShattrath()
        {
            WTLogger.Log("Taking portal to Shattrath");
            GoToTask.ToPositionAndIntecractWithGameObject(allianceDalaranPortalToShattrathPosition, allianceDalaranPortalToShattrathId);
            Thread.Sleep(5000);
        }

        /// <summary>
        /// Take horde portal from Dalaran to Shattrath
        /// </summary>
        public static void HordePortalDalaranToShattrath()
        {
            WTLogger.Log("Taking portal to Shattrath");
            GoToTask.ToPositionAndIntecractWithGameObject(dalaranPortalToShattrathPosition, dalaranPortalToShattrathId);
            Thread.Sleep(5000);
        }

    }
}
