using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    [CreateAssetMenu(fileName = "NConsoleTheme")]
    public class NConsoleTheme : ScriptableObject
    {
        [Header("Log Formats")]
        [SerializeField]
        private LogFormatInfos LogFormat = new LogFormatInfos(
            Color.white,
            "Log"
        );

        [SerializeField]
        private LogFormatInfos WarningFormat = new LogFormatInfos(
            Color.yellow,
            "Warning"
        );

        [SerializeField]
        private LogFormatInfos ErrorFormat = new LogFormatInfos(
            Color.red,
            "Error"
        );

        [SerializeField]
        private LogFormatInfos ExceptionFormat = new LogFormatInfos(
            Color.red,
            "Exception"
        );

        [SerializeField]
        private LogFormatInfos AssertFormat = new LogFormatInfos(
            Color.red,
            "Assert"
        );

        [SerializeField]
        private LogFormatInfos ConsoleFormat = new LogFormatInfos(
            Color.cyan,
            "NConsole"
        );

        [SerializeField]
        private LogFormatInfos UserFormat = new LogFormatInfos(
            Color.white,
            "User"
        );

        [Header("Elements")]
        [SerializeField] private Color m_backColor = new Color(0, 0, 0, 0.7f);

        [Header("Terminal")]
        [SerializeField] private Color m_terminalBackColor = new Color(0, 0, 0, 0.7f);
        [SerializeField] private Color m_terminalTextColor = Color.white;

        public Color BackColor => m_backColor;

        public Color TerminalBackColor => m_terminalBackColor;
        public Color TerminalTextColor => m_terminalTextColor;
        public Color TerminalPlaceHolderColor => new Color(TerminalTextColor.r, TerminalTextColor.g, TerminalTextColor.b, 0.6f);

        public LogFormatInfos MessageTypeFactory(NLogType type)
        {
            switch (type)
            {
                case NLogType.Warning:
                    return WarningFormat;

                case NLogType.Error:
                    return ErrorFormat;

                case NLogType.Exception:
                    return ExceptionFormat;

                case NLogType.Assert:
                    return AssertFormat;

                case NLogType.NConsole:
                    return ConsoleFormat;

                case NLogType.User:
                    return UserFormat;
            }

            return LogFormat;
        }
    }
}
