using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal struct ConsoleMessage
    {
        public ConsoleMessage(LogType logType, string message)
        {
            LogType = logType;

            LogInfos = ConsoleCore.MessageTypeFactory(logType);
            Message = message;
            FormattedMessage = $"[{LogInfos.Prefix}] : {message}";
        }

        public readonly LogType LogType;
        public readonly LogInfos LogInfos;
        public readonly string Message;
        public readonly string FormattedMessage;
        
        public void FormatText(TMPro.TextMeshProUGUI text)
        {
            text.text = FormattedMessage;
            text.color = LogInfos.Color;
        }
    }
}
