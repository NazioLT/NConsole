using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Nazio_LT.Tools.Console
{
    public class Terminal : TMPro.TMP_InputField
    {
        
    }

    public static class BasicCommands
    {
        [NCommand("Test Help")]
        public static void Help()
        {
            NConsole console = NConsole.Instance;

            if(console == null)
            {
                throw new System.Exception("No console instance");
            }

            Dictionary<string, MethodInfo> commands = console.Ncommands;
            string helpers = "List of all available commands : " + '\n';

            foreach (var cmd in commands.Values)
            {
                NCommandAttribute attribute = cmd.GetCustomAttribute<NCommandAttribute>();
                helpers += "- " + cmd.Name + " : " + attribute.Description + '\n';
            }

            Debug.Log(helpers);
        }
    }
}
