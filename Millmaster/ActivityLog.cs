using System;

namespace Millmaster
{
    public enum LogLevel
    {
        Info,
        Success,
        Warning
    }

    public static class ActivityLog
    {
        // The main form listens to this event
        public static event Action<DateTime, LogLevel, string> MessageAdded;

        public static void Add(string text, LogLevel level = LogLevel.Info)
        {
            if (MessageAdded != null)
            {
                MessageAdded(DateTime.Now, level, text);
            }
        }
    }
}