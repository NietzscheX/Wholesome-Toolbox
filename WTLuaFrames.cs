using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Lua Frames related methods
    /// </summary>
    public class WTLuaFrames
    {
        /// <summary>
        /// Gets the text from the StaticPopup1Text frame
        /// </summary>
        /// <returns>The text string from StaticPopup1Text or an empty string if the popup is not visible</returns>
        public static string GetStaticPopup1Text()
        {
            return Lua.LuaDoString<string>($@"
                if StaticPopup1Text:IsVisible() then
                    return StaticPopup1Text:GetText();
                end
                return ''
            ");
        }
    }
}
