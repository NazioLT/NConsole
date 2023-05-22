using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    public enum NLogType
    {
        Error = 0,
        Assert = 1,
        Warning = 2,
        Log = 3,
        Exception = 4, 
        NConsole = 5,
        User = 6,
    }

    internal struct LogInfos
    {
        public LogInfos(Color color, string prefix)
        {
            Color = color;
            Prefix = prefix;
        }

        public readonly Color Color;
        public readonly string Prefix;
    }
}
