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
        private ExecutionType m_executionMode;

        public override string ToString()
        {
            string selectedObjectInfo = (UseSelectedObject ? "(SelectedObject)" : "");
            string commandText = $"{selectedObjectInfo}{DisplayName}";
            string description = Attribute.Description == "" ? "" : (" : " + Attribute.Description);

            return commandText + description;
        }

        internal bool SetExecutionMode(ExecutionType executionMode)
        {
            m_executionMode = executionMode;

            if (m_executionMode == ExecutionType.AllMonoBehaviourInstances)
            {
                if (Method.IsStatic)
                {
                    Debug.LogWarning($"The command : {ToString()}, can't be executed because {executionMode} doesn't support static Methods.");
                    return false;
                }

                if (!typeof(MonoBehaviour).IsAssignableFrom(Method.ReflectedType))
                {
                    Debug.LogWarning($"The command : {ToString()}, can't be executed because {executionMode} methods must be in monobehaviour.");
                    return false;
                }
            }

            return true;
        }

        internal bool HasValidTarget(ref CommandContext result)
        {
            switch (m_executionMode)
            {
                case ExecutionType.Static:
                    return true;

                case ExecutionType.AllMonoBehaviourInstances:
                    {
                        object[] components = GameObject.FindObjectsOfType(Method.ReflectedType);
                        result.Targets = components;
                        return components.Length > 0;
                    }

                case ExecutionType.SelectedObjectInstance:
                    {
                        if (!NConsole.Instance.TryGetSelectedGameObject(out GameObject selectedObject))
                            return false;

                        object component = selectedObject.GetComponent(Method.ReflectedType);
                        result.SetUniqueTarget(component);
                        return component != null;
                    }
            }

            return false;
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

        private string GetArgumentDisplayName()
        {
            string arguments = "";

            for (var i = m_firstParamNameId; i < ParameterInfos.Length; i++)
            {
                var param = ParameterInfos[i];
                arguments += $" ({param.ParameterType.ToString()}){param.Name}";
            }
            return arguments;
        }

        public int ExpectedArgumentCount => ParameterInfos.Length - m_firstParamNameId;
        public string DisplayName => $"{Name} {GetArgumentDisplayName()}";

        private int m_firstParamNameId => UseSelectedObject ? 1 : 0;
    }
}
