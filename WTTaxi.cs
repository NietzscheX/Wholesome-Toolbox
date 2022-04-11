using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Flight master related methods
    /// </summary>
    public class WTTaxi
    {
        /// <summary>
        /// Returns the taxi node type (CURRENT / DISTANT / NONE / REACHABLE)
        /// </summary>
        /// <param name="taxiNodeIndex"></param>
        /// <returns>Taxi node type</returns>
        public static string GetTaxiNodeType(int taxiNodeIndex) => Lua.LuaDoString<string>($"return TaxiNodeGetType({taxiNodeIndex})");

        /// <summary>
        /// Returns the taxi node name
        /// </summary>
        /// <param name="taxiNodeIndex"></param>
        /// <returns>Taxi node name</returns>
        public static string GetTaxiNodeName(int taxiNodeIndex) => Lua.LuaDoString<string>($"return TaxiNodeName({taxiNodeIndex})");

        /// <summary>
        /// Clicks on the specified taxi node. Taxi map must be open.
        /// </summary>
        /// <param name="taxiNodeName"></param>
        public static void TakeTaxi(string taxiNodeName)
        {
            string clickNodeLua = "TakeTaxiNode(" + Lua.LuaDoString<int>("for i=0,120 do if string.find(TaxiNodeName(i),'" + taxiNodeName.Replace("'", "\\'") + "') then return i end end", "").ToString() + ")";
            Lua.LuaDoString(clickNodeLua, false);
        }
    }
}
