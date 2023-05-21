using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;
using UnityEngine;
using Nazio_LT.Tools.Console;
using TMPro;
using System.Reflection.Emit;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : MonoBehaviour
    {
        private static NConsole s_instance = null;

        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private TextMeshProUGUI m_textPrefab = null;

        private List<ConsoleMessage> m_messages = new List<ConsoleMessage>();

        private void Awake()
        {
            if (s_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            PrintLog(LogType.Log, "Initialization Succed!");
        }

        private void PrintLog(LogType type, string message)
        {
            LogInfos infos = ConsoleCore.MessageTypeFactory(type);

            string outPutMessage = $"[{infos.Prefix}] : {message}";

            TextMeshProUGUI text = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);
            text.text = outPutMessage;
            text.color = infos.Color;
        }

        private void HandleLogs(string condition, string stackTrace, LogType logType)
        {
            PrintLog(logType, condition);
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
