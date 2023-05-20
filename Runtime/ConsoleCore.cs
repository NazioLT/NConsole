using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal enum MessageType
    {
        Log,
        Warning,
        Error
    }

    internal static class ConsoleCore
    {
        public static MessageInfos MessageTypeFactory(MessageType type)
        {
            switch(type)
            {
                case MessageType.Warning: 
                    return WarningMessageInfos;

                case MessageType.Error:
                    return ErrorMessageInfos;
            }

            return LogMessageInfos;
        }   

        public static readonly MessageInfos LogMessageInfos = new MessageInfos(
            Color.white
        );

        public static readonly MessageInfos WarningMessageInfos = new MessageInfos(
            Color.yellow
        );

        public static readonly MessageInfos ErrorMessageInfos = new MessageInfos(
            Color.red
        );
    }
}
