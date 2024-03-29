using UnityEngine;
using TMPro;

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

    [System.Serializable]
    public struct LogFormatInfos
    {
        public LogFormatInfos(Color color, string prefix)
        {
            m_Color = color;
            Prefix = prefix;
            m_fontStyles = FontStyles.Normal;
        }

        [SerializeField] private Color m_Color;
        [SerializeField] private FontStyles m_fontStyles;

        public readonly string Prefix;
        
        public Color Color => m_Color;
        public FontStyles FontStyles => m_fontStyles;
    }
}
