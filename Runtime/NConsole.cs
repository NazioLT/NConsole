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
        private Dictionary<string, NCommandPolymorphism> m_ncommands = new Dictionary<string, NCommandPolymorphism>();

        public void ErrorMessage(string message)
        {
            HandleLog(message, "", NLogType.Error);
        }

        internal void UserMessage(string message)
        {
            HandleLog(message, "", NLogType.User);
        }

        public void Clear()
        {
            if (m_messages == null || m_messages.Count == 0) return;

            for (int i = 0; i < m_messages.Count; i++)
            {
                Destroy(m_messages[i].gameObject);
            }

            m_messages.Clear();
        }

        protected override void Start()
        {
            base.Start();

            if (!Application.isPlaying) return;

            if (s_instance)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            if (!Application.isPlaying) return;

            m_clearButton.onClick.AddListener(Clear);
            m_terminal.onSubmit.AddListener(EnterCommand);

            m_ncommands = ConsoleCore.GetAllNCommands();
            HandleLog($"{m_ncommands.Count} commands registered!", "", NLogType.NConsole);

            HandleLog("NConsole initialization succed!", "", NLogType.NConsole);
        }

        private void EnterCommand(string input)
        {
            if (!Application.isPlaying) return;

            m_terminal.ActivateInputField();
            m_terminal.Select();

            if (input == "") return;

            UserMessage(input);

            m_terminal.text = "";

            string[] tokens = input.Split(' ');
            string commandText = tokens[0];

            if (!m_ncommands.ContainsKey(commandText))
            {
                ErrorMessage("Unknown command.");
                return;
            }

            NCommandPolymorphism command = m_ncommands[commandText];

            if (command.HasValidMethod(tokens, out CommandContext result))
            {
                result.Invoke();
                return;
            }

            ErrorMessage($"{result.Error} Type Help to see all registered commands.");
            ErrorMessage($"The most similar commands are : \n{command.ToString()}");
        }

        private void HandleLog(string condition, string stackTrace, LogType logType)
        {
            HandleLog(condition, stackTrace, (NLogType)logType);
        }

        private void HandleLog(string condition, string stackTrace, NLogType logType)
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

        internal Dictionary<string, NCommandPolymorphism> Ncommands => m_ncommands;
    }
}
