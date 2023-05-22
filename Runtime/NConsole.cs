using System.Collections.Generic;
using System.Reflection;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : Selectable, ISubmitHandler, IEventSystemHandler
    {
        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private NLog m_textPrefab = null;
        [SerializeField] private Button m_clearButton = null;
        [SerializeField] private Terminal m_terminal = null;

        private static NConsole s_instance = null;

        private List<NLog> m_messages = new List<NLog>();
        private Dictionary<string, NCommand> m_ncommands = new Dictionary<string, NCommand>();

        public void ErrorMessage(string message)
        {
            HandleLog(message, "", LogType.Error);
        }

        protected override void Start()
        {
            base.Start();

            if(!Application.isPlaying) return;

            if(s_instance)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            if (!Application.isPlaying) return;

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
            if(!Application.isPlaying) return;

            m_terminal.ActivateInputField();
            m_terminal.Select();

            if (input == "") return;

            m_terminal.text = "";

            string[] tokens = input.Split(' ');
            string commandText = tokens[0];

            if (!m_ncommands.ContainsKey(commandText))
            {
                ErrorMessage("Unknown command.");
                return;
            }

            NCommand command = m_ncommands[commandText];
            MethodInfo method = command.Method;
            ParameterInfo[] parameters = command.ParameterInfos;

            if (parameters.Length != tokens.Length - 1)
            {
                ErrorMessage($"Incorrect argument count. {commandText} contains {parameters.Length} arguments. " + command.ToString());
                return;
            }

            if (parameters.Length == 0)
            {
                Debug.Log(commandText);
                method.Invoke(null, null);
                return;
            }

            object[] arguments = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                if (!ConsoleCore.IsArgumentValid(parameters[i], tokens[i + 1], out object argument))
                {
                    ErrorMessage($"Argument number {i + 1} is incorrect. A {parameters[i].ParameterType} argument is expected. " + command.ToString());
                    return;
                }

                arguments[i] = argument;
            }

            Debug.Log(commandText);
            method.Invoke(null, arguments);
        }

        private void HandleLog(string condition, string stackTrace, LogType logType)
        {
            NLog log = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);

            ConsoleMessage message = new ConsoleMessage(logType, condition);
            log.Format(message);

            m_messages.Add(log);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            m_terminal.DeactivateInputField();

            base.OnDeselect(eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            m_terminal.ActivateInputField();

            base.OnSelect(eventData);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Application.logMessageReceived += HandleLog;
        }

        protected override void OnDisable()
        {
            base.OnEnable();
            Application.logMessageReceived -= HandleLog;
        }

        public void OnSubmit(BaseEventData eventData)
        {
            EnterCommand(m_terminal.text);
        }

        public static NConsole Instance => s_instance;

        internal Dictionary<string, NCommand> Ncommands => m_ncommands;
    }
}
