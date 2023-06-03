using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    /// <summary>
    /// All send commands history.
    /// </summary>
    internal class ConsoleHistory
    {
        private List<string> m_history = new List<string>();
        private int m_currentID = -1;
        private string m_currentMessage = "";
        private bool m_hasWrited = false;

        public void Add(string message)
        {
            m_history.Add(message);
            m_currentID = -1;
        }

        public string ChangeSelectedMessageID(bool up)
        {
            int change = up ? 1 : -1;

            m_currentID = Mathf.Clamp(m_currentID + change, -1, m_history.Count -1);

            return GetCurrent();
        }

        public string GetCurrent()
        {
            if (m_currentID == -1)
                return m_currentMessage;

            if (m_history.Count == 0)
                return "";

            return m_history[m_currentID];
        }
    }
}
