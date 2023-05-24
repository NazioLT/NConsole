using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Nazio_LT.Tools.Console
{
    public class AutoCompletion : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text = null;

        private Dictionary<string, NCommandPolymorphism> m_ncommands = new Dictionary<string, NCommandPolymorphism>();

        private void Start()
        {
            m_ncommands = NConsole.Instance.Ncommands;
        }

        public void SetInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                text.text = "";
                return;
            }

            List<string> availablesCommands = new List<string>();
            foreach (var command in m_ncommands.Keys)
            {
                if (command.Contains(input))
                {
                    availablesCommands.Add(command);
                }
            }

            if (availablesCommands.Count == 0)
            {
                text.text = "";
                return;
            }

            List<string> nearCommands = new List<string>();
            foreach (var command in nearCommands)
            {

                if (command.Substring(0, input.Length) == input)
                {
                    print(command.Substring(0, input.Length));
                    nearCommands.Add(command);
                }
            }

            if (nearCommands.Count == 0)
            {
                text.text = availablesCommands[0];
                return;
            }

            text.text = nearCommands[0];
        }
    }
}
