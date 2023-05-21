using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal static class ConsoleCore
    {
        public static LogInfos MessageTypeFactory(LogType type)
        {
            switch (type)
            {
                case LogType.Warning:
                    return WarningMessageInfos;

                case LogType.Error:
                    return ErrorMessageInfos;

                case LogType.Exception:
                    return ExceptionMessageInfos;

                case LogType.Assert:
                    return AssertMessageInfos;
            }

            return LogMessageInfos;
        }

        public static readonly LogInfos LogMessageInfos = new LogInfos(
            Color.white,
            "Log"
        );

        public static readonly LogInfos WarningMessageInfos = new LogInfos(
            Color.yellow,
            "Warning"
        );

        public static readonly LogInfos ErrorMessageInfos = new LogInfos(
            Color.red,
            "Error"
        );

        public static readonly LogInfos ExceptionMessageInfos = new LogInfos(
            Color.red,
            "Exception"
        );

        public static readonly LogInfos AssertMessageInfos = new LogInfos(
            Color.red,
            "Assert"
        );
    }
}
