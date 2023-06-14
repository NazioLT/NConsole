using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    /// <summary>
    /// Representation of a log.
    /// </summary>
    public class NLog : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_logText = null;

        private ConsoleMessage m_message;

        internal void Format(ConsoleMessage message)
        {
            m_message = message;
            message.FormatText(m_logText);
        }

        internal void UpdateTheme(NConsoleTheme theme)
        {
            LogFormatInfos format = theme.MessageTypeFactory(m_message.LogType);
            m_message.SetFormat(format);

            Format(m_message);
        }
    }
}
