using System.Reflection;

namespace Nazio_LT.Tools.Console
{
    internal struct NCommand
    {
        internal string Name;
        internal NCommandAttribute Attribute;
        internal MethodInfo Method;
        internal ParameterInfo[] ParameterInfos;

        internal bool IsArgumentsValid(string[] tokens, out CommandContext result)
        {
            result = new CommandContext(Method, ParameterInfos.Length);

            if (ParameterInfos.Length == 0 && tokens.Length == 1)
            {
                return true;
            }

            for (int i = 0; i < ParameterInfos.Length; i++)
            {
                if (!ConsoleCore.IsArgumentValid(ParameterInfos[i], tokens[i + 1], out object argument))
                {
                    result.Error = $"Argument number {i + 1} is incorrect. A {ParameterInfos[i].ParameterType} argument is expected. " + ToString();
                    return false;
                }

                result.Arguments[i] = argument;
            }

            return true;
        }

        public override string ToString()
        {
            string arguments = "";

            foreach (var param in ParameterInfos)
            {
                arguments += $" ({param.ParameterType.ToString()}){param.Name}";
            }

            string commandText = Name + arguments;
            string description = Attribute.Description == "" ? "" : (" : " + Attribute.Description);

            return commandText + description;
        }
    }
}
