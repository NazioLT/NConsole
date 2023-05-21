using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : MonoBehaviour
    {
        private static NConsole s_instance = null;

        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private NLog m_textPrefab = null;
        [SerializeField] private Button m_clearButton = null;
        [SerializeField] private TMPro.TMP_InputField m_terminal = null;

        private List<NLog> m_messages = new List<NLog>();
        private Dictionary<string, MethodInfo> m_ncommands = new Dictionary<string, MethodInfo>();

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
            m_terminal.onSubmit.AddListener(EnterCommand);

            m_ncommands = ConsoleCore.GetAllNCommands();

            HandleLog("NConsole initialization succed!", "", LogType.Log);
        }

        private void Clear()
        {
            if (m_messages == null || m_messages.Count == 0) return;

            for (int i = 0; i < m_messages.Count; i++)
            {
                Destroy(m_messages[i].gameObject);
            }

            m_messages.Clear();
        }

        private void EnterCommand(string command)
        {
            if (command == "") return;

            m_terminal.text = "";

            if (!m_ncommands.ContainsKey(command))
            {
                ErrorMessage("Unknown command.");
                return;
            }

            Debug.Log(command);
        }

        private void HandleLog(string condition, string stackTrace, LogType logType)
        {
            NLog log = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);

            ConsoleMessage message = new ConsoleMessage(logType, condition);
            log.Format(message);

            m_messages.Add(log);
        }

        private void ErrorMessage(string message)
        {
            HandleLog(message, "", LogType.Error);
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
