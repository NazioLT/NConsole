using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : MonoBehaviour
    {
        private static NConsole s_instance = null;

        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private NLog m_textPrefab = null;

        private List<NLog> m_messages = new List<NLog>();

        private void Awake()
        {
            if (s_instance != null)
            {
                HandleLogs("Another NConsole instance exist!", "", LogType.Warning);
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            HandleLogs("NConsole initialization succed!", "", LogType.Log);
        }

        private void HandleLogs(string condition, string stackTrace, LogType logType)
        {
            NLog log = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);

            ConsoleMessage message = new ConsoleMessage(logType, condition);
            log.Format(message);

            m_messages.Add(log);
        }

        private void OnEnable()
        {
            Application.logMessageReceived += HandleLogs;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= HandleLogs;
        }

        public static NConsole Instance => s_instance;
    }
}
