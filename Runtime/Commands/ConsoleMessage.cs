namespace Nazio_LT.Tools.Console
{
    /// <summary>
    /// Define the message display.
    /// </summary>
    internal struct ConsoleMessage
    {
        public ConsoleMessage(NLogType logType, string message)
        {
            LogType = logType;

            LogInfos = NConsole.Instance.Theme.MessageTypeFactory(logType);
            Message = message;
            FormattedMessage = $"[{LogInfos.Prefix}] : {message}";
        }

        public readonly NLogType LogType;
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
