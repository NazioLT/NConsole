using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    public static class BasicCommands
    {
        [NCommand(Description = "Description Help")]
        public static void Help()
        {
            NConsole console = NConsole.Instance;

            if(console == null)
            {
                throw new System.Exception("No console instance");
            }

            Dictionary<string, NCommand> commands = console.Ncommands;
            string helpers = "List of all available commands : " + '\n';

            foreach (var cmd in commands.Values)
            {
                NCommandAttribute attribute = cmd.Method.GetCustomAttribute<NCommandAttribute>();
                helpers += "- " + cmd.Name + " : " + attribute.Description + '\n';
            }

            Debug.Log(helpers);
        }
    }
}
