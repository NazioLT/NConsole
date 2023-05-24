using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public class AutoCompletion : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] m_texts = null;

        private string m_mostProbableCommand = "";

        private Dictionary<string, NCommandPolymorphism> m_ncommands = new Dictionary<string, NCommandPolymorphism>();

        public void SetInput(string input)
        {
            DisableAllTexts();

            if (string.IsNullOrWhiteSpace(input))
                return;

            string[] tokens = ConsoleCore.GetTokens(input);

            List<string> availablesCommands = new List<string>();
            foreach (var command in m_ncommands.Keys)
            {
                if(command == tokens[0])
                    continue;

                if (command.Contains(tokens[0]))
                {
                    availablesCommands.Add(command);
                }
            }

            if (availablesCommands.Count == 0)
                return;

            List<string> finalCommands = new List<string>();
            List<string> otherCommands = new List<string>();
            foreach (var command in availablesCommands)
            {
                if (command.Substring(0, tokens[0].Length) == tokens[0])
                {
                    finalCommands.Add(command);
                    continue;
                }
                otherCommands.Add(command);
            }

            if (finalCommands.Count < m_texts.Length)
            {
                for (var i = 0; i < otherCommands.Count; i++)
                {
                    if(finalCommands.Count == m_texts.Length)
                        break;

                    finalCommands.Add(otherCommands[i]);
                }
            }

            SetTexts(finalCommands.ToArray());
            m_mostProbableCommand = finalCommands.Count > 0 ? finalCommands[0] : "";
        }
        
        private void Start()
        {
            m_ncommands = NConsole.Instance.Ncommands;
            DisableAllTexts();
        }

        private void DisableAllTexts()
        {
            foreach (var text in m_texts)
            {
                text.gameObject.SetActive(false);
            }
        }

        private void SetTexts(string[] texts)
        {
            for (var i = 0; i < texts.Length; i++)
            {
                m_texts[i].gameObject.SetActive(true);
                m_texts[i].text = texts[i];
            }
        }

        public string MostProbableCommand => m_mostProbableCommand;
    }
}
