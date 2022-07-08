using wManager.Wow.Helpers;

namespace WholesomeToolbox
{
    /// <summary>
    /// Quest log related methods
    /// </summary>
    public class WTQuestLog
    {
        /// <summary>
        /// Expand a specific quest header, are all if index is 0
        /// </summary>
        /// <param name="index"></param>
        public static void ExpandQuestHeader(int index = 0) => Lua.LuaDoString($"ExpandQuestHeader({index});");        

        /// <summary>
        /// Returns the log index of a quest by ID
        /// </summary>
        /// <param name="questId"></param>
        /// <returns>Log index</returns>
        public static int GetLogQuestIndexByQuestId(int questId)
        {
            ExpandQuestHeader();
            return Lua.LuaDoString<int>($@"
                local nbLogQuests  = GetNumQuestLogEntries()
                for i=1, nbLogQuests do
                    local _, _, _, _, _, _, _, _, questID = GetQuestLogTitle(i);
                    if questID == {questId} then
                        return i;
                    end
                end
            ");
        }

        /// <summary>
        /// Abandons a quest from the player's log
        /// </summary>
        /// <param name="questId"></param>
        public static void AbandonQuest(int questId)
        {
            int logIndex = GetLogQuestIndexByQuestId(questId);
            Lua.LuaDoString($"SelectQuestLogEntry({logIndex}); SetAbandonQuest(); AbandonQuest();");
        }
    }
}
