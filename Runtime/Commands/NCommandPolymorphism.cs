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

        public override string ToString()
        {
            string result = "";
            foreach (var command in m_commands)
            {
                result += "- " + command.ToString() + '\n';
            }

            return result;
        }

        internal void Add(NCommand command)
        {
            m_commands.Add(command);
        }

        internal bool HasValidCommand(string[] tokens, out CommandContext result)
        {
            result = new CommandContext(null, 0);
            int argumentCount = tokens.Length - 1;

            List<NCommand> argumentCountedCommand = GetAllArgumentCountValidCommands(argumentCount);

            if (argumentCountedCommand.Count == 0)
            {
                result.Error = $"Argument count exception. No {m_commands[0].Name} contains {argumentCount} arguments.";
                return false;
            }

            foreach (NCommand command in argumentCountedCommand)
            {
                if (command.AreArgumentsValid(tokens, out result))
                {
                    if (command.HasValidTarget(ref result))
                        return true;

                    return false;
                }
            }

            return false;
        }

        private readonly List<NCommand> GetAllArgumentCountValidCommands(int argumentCount)
        {
            List<NCommand> argumentCountedCommand = new List<NCommand>();
            foreach (var command in m_commands)
            {
                if (command.ExpectedArgumentCount == argumentCount)
                {
                    argumentCountedCommand.Add(command);
                }
            }

            return argumentCountedCommand;
        }
    }
}
