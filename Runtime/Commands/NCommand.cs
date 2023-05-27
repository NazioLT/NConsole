using System.Reflection;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    /// <summary>
    /// All data of a command.
    /// </summary>
    internal struct NCommand
    {
        internal string Name;
        internal NCommandAttribute Attribute;
        internal MethodInfo Method;
        internal ParameterInfo[] ParameterInfos;
        internal bool UseSelectedObject;

        public override string ToString()
        {
            string arguments = "";

            for (var i = m_firstParamNameId; i < ParameterInfos.Length; i++)
            {
                var param = ParameterInfos[i];
                arguments += $" ({param.ParameterType.ToString()}){param.Name}";
            }

            string selectedObjectInfo = (UseSelectedObject ? "(SelectedObject)" : "");
            string commandText = $"{selectedObjectInfo}{Name} {arguments}";
            string description = Attribute.Description == "" ? "" : (" : " + Attribute.Description);

            return commandText + description;
        }

        internal bool AreArgumentsValid(string[] tokens, out CommandContext result)
        {
            int unUsedArgCount = m_firstParamNameId + 1;
            int cmdArgsCount = tokens.Length - unUsedArgCount;

            result = new CommandContext(Method, ParameterInfos.Length);

            if (ParameterInfos.Length == 0 && cmdArgsCount == 0)
                return true;

            if (!EachArgumentsAreCorrect(tokens, ref result))
                return false;

            if (UseSelectedObject && !HasValidGameObjectSelected(ref result))
                return false;

            return true;
        }

        private bool HasValidGameObjectSelected(ref CommandContext result)
        {
            if (!NConsole.Instance.TryGetSelectedGameObject(out GameObject obj))
            {
                result.Error = "No Object Selected. Type : Select 'ObjectName' to select an object.";
                result.OtherCommandsPurposes = false;
                return false;
            }

            result.Arguments[0] = obj;
            return true;
        }

        private bool EachArgumentsAreCorrect(string[] tokens, ref CommandContext result)
        {
            int tokenIDDelta = 1 - m_firstParamNameId;
            for (int i = m_firstParamNameId; i < ParameterInfos.Length; i++)
            {
                if (!ConsoleCore.IsArgumentValid(ParameterInfos[i], tokens[i + tokenIDDelta], out object argument))
                {
                    result.Error = $"Argument number {i + tokenIDDelta} is incorrect. A {ParameterInfos[i].ParameterType} argument is expected. " + ToString();
                    return false;
                }

                result.Arguments[i] = argument;
            }

            return true;
        }

        public int ExpectedArgumentCount => ParameterInfos.Length - m_firstParamNameId;

        private int m_firstParamNameId => UseSelectedObject ? 1 : 0;
    }
}
