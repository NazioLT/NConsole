using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public class AutoCompletionText : TextMeshProUGUI
    {
        internal void LinkCommand(string command)
        {
            text = command;
        }

        internal void Select(bool value)
        {
            color = value ? Color.white : Color.grey;
        }
    }
}
