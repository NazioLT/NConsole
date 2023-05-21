using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public class NLog : MonoBehaviour
    {   
        [SerializeField] private TextMeshProUGUI m_logText = null;

        private ConsoleMessage m_message;

        internal void Format(ConsoleMessage message)
        {
            m_message = message;
            message.FormatText(m_logText);
        }
    }
}
