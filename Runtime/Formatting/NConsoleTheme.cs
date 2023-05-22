using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    [CreateAssetMenu(fileName = "NConsoleTheme")]
    public class NConsoleTheme : ScriptableObject
    {
        [SerializeField]
        private LogInfos LogFormat = new LogInfos(
            Color.white,
            "Log"
        );

        [SerializeField]
        private LogInfos WarningFormat = new LogInfos(
            Color.yellow,
            "Warning"
        );

        [SerializeField]
        private LogInfos ErrorFormat = new LogInfos(
            Color.red,
            "Error"
        );

        [SerializeField]
        private LogInfos ExceptionFormat = new LogInfos(
            Color.red,
            "Exception"
        );

        [SerializeField]
        private LogInfos AssertFormat = new LogInfos(
            Color.red,
            "Assert"
        );

        [SerializeField]
        private LogInfos ConsoleFormat = new LogInfos(
            Color.cyan,
            "NConsole"
        );

        [SerializeField]
        private LogInfos UserFormat = new LogInfos(
            Color.white,
            "User"
        );

        public LogInfos MessageTypeFactory(NLogType type)
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
