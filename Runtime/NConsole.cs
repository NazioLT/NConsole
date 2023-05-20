using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System;
using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public unsafe class NConsole : MonoBehaviour
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
        }

        public void Log(string message)
        {
            PrintMessage(MessageType.Log, message);
        }

        public void Warning(string message)
        {
            PrintMessage(MessageType.Warning, message);
        }

        public void Error(string message)
        {
            PrintMessage(MessageType.Error, message);
        }

        private void PrintMessage(MessageType type, string message)
        {
            MessageInfos infos = ConsoleCore.MessageTypeFactory(type);

            TextMeshProUGUI text = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);
            text.text = message;
            text.color = infos.Color;
        }

        public static NConsole Instance => s_instance;
    }
}
