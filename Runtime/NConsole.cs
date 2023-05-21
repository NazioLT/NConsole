using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : MonoBehaviour
    {
        private static NConsole s_instance = null;

        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private NLog m_textPrefab = null;
        [SerializeField] private Button m_clearButton = null;

        private List<NLog> m_messages = new List<NLog>();

        private void Awake()
        {
            if (s_instance != null)
            {
                HandleLog("Another NConsole instance exist!", "", LogType.Warning);
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            m_clearButton.onClick.AddListener(Clear);

            HandleLog("NConsole initialization succed!", "", LogType.Log);
        }

        private void Clear()
        {
            if(m_messages == null || m_messages.Count == 0) return;

            for (int i = 0; i < m_messages.Count; i++)
            {
                Destroy(m_messages[i].gameObject);
            }

            m_messages.Clear();
        }

        private void HandleLog(string condition, string stackTrace, LogType logType)
        {
            NLog log = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);

            ConsoleMessage message = new ConsoleMessage(logType, condition);
            log.Format(message);

            m_messages.Add(log);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        public static NConsole Instance => s_instance;
    }
}
