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
            string[] propositions = null;

            if (tokens.Length == 1 && input[input.Length - 1] != ' ')//Search per character
            {
                if (!GetCommandPropositions(tokens, out propositions))
                    return;
            }
            else
            {
                if (!GetCommandPropositionsStrict(tokens, out propositions))
                    return;
            }

            SetPropositionTexts(propositions);
            m_mostProbableCommand = propositions.Length > 0 ? propositions[0] : "";
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

        private void SetPropositionTexts(string[] texts)
        {
            for (var i = 0; i < texts.Length; i++)
            {
                m_texts[i].gameObject.SetActive(true);
                m_texts[i].text = texts[i];
            }
        }

        private bool GetCommandPropositionsStrict(string[] tokens, out string[] propositions)
        {
            propositions = null;

            if(!m_ncommands.ContainsKey(tokens[0]))
                return false;

            NCommandPolymorphism polymorphism = m_ncommands[tokens[0]];
            List<NCommand> commands = polymorphism.GetAllWithMinimumArgumentCount(tokens.Length - 1);

            if(commands == null || commands.Count == 0)
                return false;

            propositions = new string[commands.Count];
            for (var i = 0; i < commands.Count; i++)
            {
                propositions[i] = commands[i].DisplayName;
            }

            return true;
        }

        private bool GetCommandPropositions(string[] tokens, out string[] propositions)
        {
            propositions = new string[0];

            List<string> availablesCommands = new List<string>();
            foreach (string command in m_ncommands.Keys)
            {
                if (command == tokens[0])
                    continue;

                if (command.Contains(tokens[0]))
                {
                    availablesCommands.Add(command);
                }
            }

            if (availablesCommands.Count == 0)
                return false;

            //Trie les meilleurs commandes.
            List<string> finalCommands = new List<string>();
            List<string> otherCommands = new List<string>();
            foreach (string command in availablesCommands)
            {
                if (command.Substring(0, tokens[0].Length) == tokens[0])
                {
                    finalCommands.Add(command);
                    continue;
                }
                otherCommands.Add(command);
            }

            //Complete par d'autres commandes.
            if (finalCommands.Count < m_texts.Length)
            {
                for (var i = 0; i < otherCommands.Count; i++)
                {
                    if (finalCommands.Count == m_texts.Length)
                        break;

                    finalCommands.Add(otherCommands[i]);
                }
            }

            propositions = finalCommands.ToArray();
            return true;
        }

        public string MostProbableCommand => m_mostProbableCommand;
    }
}
