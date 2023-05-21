using UnityEngine;

namespace Nazio_LT.Tools.Console
{
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
