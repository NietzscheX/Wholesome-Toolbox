using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Lua related methods
    /// </summary>
    public static class WTLua
    {
        /// <summary>
        /// Extension method to escape a LUA string (ex : questName.EscapeLuaString())
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Escaped LUA string</returns>
        public static string EscapeLuaString(this string str) => str.Replace("\\", "\\\\").Replace("'", "\\'");

        /// <summary>
        /// Returns the current WoW version
        /// </summary>
        /// <returns>WoW version (ex: "2.4.3")</returns>
        public static string GetWoWVersion => Lua.LuaDoString<string>("v, _, _, _ = GetBuildInfo(); return v");

        /// <summary>
        /// Clicks on a frame button
        /// ex: ClickOnFrameButton("StaticPopup1Button1")
        /// </summary>
        /// <param name="button"></param>
        public static void ClickOnFrameButton(string button)
        {
            Lua.LuaDoString($@"
                    if GetClickFrame('{button}'):IsVisible() then
                        {button}:Click();
                    end
                ");
        }
    }
}
