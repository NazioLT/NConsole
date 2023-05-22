using System.Collections.Generic;

namespace Nazio_LT.Tools.Console
{
    internal struct NCommandPolymorphism
    {
        internal NCommandPolymorphism(NCommand command)
        {
            m_commands = new List<NCommand>();
            m_commands.Add(command);
        }

        private List<NCommand> m_commands;

        public void Add(NCommand command)
        {
            m_commands.Add(command);
        }

        public bool HasValidMethod(string[] tokens, out CommandContext result)
        {
            result = new CommandContext(null, 0);
            int argumentCount = tokens.Length - 1;

            // Check Argument count
            List<NCommand> argumentCountedCommand = new List<NCommand>();
            foreach (var command in m_commands)
            {
                if(command.ParameterInfos.Length == argumentCount)
                {
                    argumentCountedCommand.Add(command);
                }
            }

            if(argumentCountedCommand.Count == 0)
            {
                result.Error = $"Argument count exception. No {m_commands[0].Name} contains {argumentCount} arguments.";
                return false;
            }

            foreach (var command in argumentCountedCommand)
            {
                if(command.IsArgumentsValid(tokens, out result))
                {
                    return true;
                }

            }

            return false;
        }

        public override string ToString()
        {
            string result = "";
            foreach (var command in m_commands)
            {
                result += "- " + command.ToString() + '\n';   
            }

            return result;
        }
    }
}
