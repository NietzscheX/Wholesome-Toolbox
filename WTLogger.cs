using robotManager.Helpful;
using System.Drawing;

namespace WholesomeToolbox
{
    /// <summary>
    /// Internal logger
    /// </summary>
    internal class WTLogger
    {
        /// <summary>
        /// Normal log
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            Logging.Write($"[Wholesome Toolbox] {str}", Logging.LogType.Normal, Color.Chocolate);
        }

        /// <summary>
        /// Error log
        /// </summary>
        /// <param name="str"></param>
        public static void LogError(string str)
        {
            Logging.Write($"[Wholesome Toolbox] {str}", Logging.LogType.Normal, Color.Red);
        }
    }
}
