using System.Collections.Generic;
using System.Reflection;
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
        [SerializeField] private TMPro.TMP_InputField m_terminal = null;

        private List<NLog> m_messages = new List<NLog>();
        private Dictionary<string, MethodInfo> m_ncommands = new Dictionary<string, MethodInfo>();

        public void ErrorMessage(string message)
        {
            HandleLog(message, "", LogType.Error);
        }

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

        private void EnterCommand(string input)
        {
            m_terminal.ActivateInputField();
            m_terminal.Select();

            if (input == "") return;

            m_terminal.text = "";

            string[] tokens = input.Split(' ');
            string command = tokens[0];

            if (!m_ncommands.ContainsKey(command))
            {
                ErrorMessage("Unknown command.");
                return;
            }

            MethodInfo method = m_ncommands[command];
            ParameterInfo[] parameters = method.GetParameters();

            if(parameters.Length != tokens.Length - 1)
            {
                ErrorMessage($"Incorrect argument count. {command} contains {parameters.Length} arguments.");
                return;
            }

            if (parameters.Length == 0)
            {
                Debug.Log(command);
                m_ncommands[command].Invoke(null, null);
                return;
            }

            object[] arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if(!ConsoleCore.IsArgumentValid(parameters[i], tokens[i + 1], out object argument))
                {
                    ErrorMessage($"Argument number {i + 1} is incorrect.");
                    return;
                }

                arguments[i] = argument;
            }

            Debug.Log(command);
            m_ncommands[command].Invoke(null, arguments);
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
