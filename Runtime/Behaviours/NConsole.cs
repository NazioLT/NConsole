using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    [DefaultExecutionOrder(-100)]
    public sealed class NConsole : Selectable, ISubmitHandler, IEventSystemHandler
    {
        [SerializeField] private NConsoleTheme m_theme = null;
        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private NLog m_textPrefab = null;
        [SerializeField] private Button m_clearButton = null;
        [SerializeField] private Terminal m_terminal = null;
        [SerializeField] private AutoCompletion m_autoCompletion = null;

        private static NConsole s_instance = null;

        //References
        private Image m_terminalBack = null;
        private Image m_consoleBack = null;

        //Commands
        private List<NLog> m_messages = new List<NLog>();
        private Dictionary<string, NCommandPolymorphism> m_ncommands = new Dictionary<string, NCommandPolymorphism>();

        //Typed Commands
        private List<string> m_sendCommand = new List<string>();
        private int m_sendCommandId = 0;
        private string m_writedLine = "";

        //Use by commands
        private GameObject m_selectedObject = null;

        #region Public

        public bool TryGetSelectedGameObject(out GameObject obj)
        {
            obj = m_selectedObject;
            if (obj == null)
            {
                Debug.Log("No Object Selected.");
                return false;
            }

            return true;
        }

        public void ErrorMessage(string message)
        {
            HandleLog(message, "", NLogType.Error);
        }

        public void ClearConsole()
        {
            if (m_messages == null || m_messages.Count == 0) return;



            m_messages.Clear();
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

        public void OnSubmit(BaseEventData eventData)
        {
            EnterCommand(m_terminal.text);
        }

        #endregion

        #region Inputs

        internal void Tab()
        {
            string mostProbableCmd = m_autoCompletion.MostProbableCommand;

            if (string.IsNullOrWhiteSpace(mostProbableCmd))
                return;

            m_terminal.text += ' ';

            m_terminal.text = mostProbableCmd;
            m_terminal.MoveTextEnd(false);
        }

        internal void ArrowInput(bool up)
        {
            if (!string.IsNullOrWhiteSpace(m_terminal.text))
            {
                AutoCompletionArrowInput(up);
                return;
            }

            if (m_sendCommand.Count == 0)
                return;

            HistoricCommandArrowInput(up);
        }

        private void AutoCompletionArrowInput(bool up)
        {
            m_autoCompletion.ArrowInput(up);
        }

        private void HistoricCommandArrowInput(bool up)
        {
            if (up)
            {
                if (m_sendCommandId == 0)
                    return;

                if (m_sendCommandId >= m_sendCommand.Count)
                {
                    m_writedLine = m_terminal.text;
                }

                m_sendCommandId--;
            }
            else
            {
                m_sendCommandId++;

                if (m_sendCommandId >= m_sendCommand.Count)
                {
                    m_terminal.text = m_writedLine;
                    m_sendCommandId = m_sendCommand.Count;
                    return;
                }
            }

            m_terminal.text = m_sendCommand[m_sendCommandId];
        }

        #endregion

        internal void UserMessage(string message)
        {
            HandleLog(message, "", NLogType.User);
        }

        internal void SetSelectedGameObject(GameObject obj)
        {
            m_selectedObject = obj;
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

        protected override void Start()
        {
            if (m_theme == null)
            {
                Debug.LogError("Console has no theme.");
                return;
            }

            m_terminalBack = m_terminal.GetComponent<Image>();
            m_consoleBack = GetComponentInChildren<ScrollRect>().GetComponent<Image>();

            ApplyTheme();

            base.Start();

            if (!Application.isPlaying) return;

            if (s_instance)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;


            m_clearButton.onClick.AddListener(ClearConsole);
            m_terminal.onSubmit.AddListener(EnterCommand);
            m_terminal.onValueChanged.AddListener(OnInputFieldValueChange);

            m_ncommands = ConsoleCore.GetAllNCommands();
            HandleLog($"{m_ncommands.Count} commands registered!", "", NLogType.NConsole);

            HandleLog("NConsole initialization succed!", "", NLogType.NConsole);
        }

        private void ApplyTheme()
        {
            NConsoleThemeBehaviour[] objectsToColor = GetComponentsInChildren<NConsoleThemeBehaviour>();
            foreach (var obj in objectsToColor)
            {
                obj.ApplyTheme(m_theme);
            }
        }

        private void EnterCommand(string input)
        {
            if (!Application.isPlaying) return;

            m_terminal.ActivateInputField();
            m_terminal.Select();

            if (input == "") return;

            UserMessage(input);
            m_sendCommand.Add(input);
            m_sendCommandId = m_sendCommand.Count;

            m_terminal.text = "";

            string[] tokens = ConsoleCore.GetTokens(input);

            string commandText = tokens[0];

            if (!m_ncommands.ContainsKey(commandText))
            {
                ErrorMessage("Unknown command.");
                return;
            }

            NCommandPolymorphism command = m_ncommands[commandText];

            if (command.HasValidCommand(tokens, out CommandContext result))
            {
                result.Invoke();
                return;
            }

            ErrorMessage($"{result.Error} Type Help to see all registered commands.");
            if (result.OtherCommandsPurposes)
                ErrorMessage($"The most similar commands are : \n{command.ToString()}");
        }

        private void HandleLog(string condition, string stackTrace, LogType logType)
        {
            if (!Application.isPlaying) return;

            HandleLog(condition, stackTrace, (NLogType)logType);
        }

        private void HandleLog(string condition, string stackTrace, NLogType logType)
        {
            if (!Application.isPlaying) return;

            NLog log = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);

            ConsoleMessage message = new ConsoleMessage(logType, condition);
            log.Format(message);

            m_messages.Add(log);
        }

        private void OnInputFieldValueChange(string text)
        {
            m_autoCompletion.SetInput(text);
        }

        public static NConsole Instance => s_instance;

        public NConsoleTheme Theme => m_theme;

        internal Dictionary<string, NCommandPolymorphism> Ncommands => m_ncommands;
    }
}
