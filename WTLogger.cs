using robotManager.Helpful;
using System.Drawing;

namespace WholesomeToolbox
{
    internal class WTLogger
    {
        public static void Log(string str)
        {
            Logging.Write($"[Wholesome Toolbox] {str}", Logging.LogType.Normal, Color.Chocolate);
        }
        public static void LogError(string str)
        {
            Logging.Write($"[Wholesome Toolbox] {str}", Logging.LogType.Normal, Color.Red);
        }
    }
}
