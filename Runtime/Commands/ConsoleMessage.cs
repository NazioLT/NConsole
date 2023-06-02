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

            m_logFormat = NConsole.Instance.Theme.MessageTypeFactory(logType);
            Message = message;
            FormattedMessage = $"[{m_logFormat.Prefix}] : {message}";
        }

        private LogFormatInfos m_logFormat;

        public readonly NLogType LogType;
        public readonly string Message;
        public readonly string FormattedMessage;
        
        public void FormatText(TMPro.TextMeshProUGUI text)
        {
            text.text = FormattedMessage;
            text.color = m_logFormat.Color;
        }

        public void SetFormat(LogFormatInfos format)
        {
            m_logFormat = format;
        }
    }
}
