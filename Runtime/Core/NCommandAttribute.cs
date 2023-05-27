using System;

namespace Nazio_LT.Tools.Console
{
    public enum ExecutionType
    {
        Static = 0,
        AllMonoBehaviourInstances = 1,
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class NCommandAttribute : Attribute
    {
        public string Description = "";
        public string CustomName = "";
        public bool UseSelectedObject = false;
        public ExecutionType ExecutionMode = ExecutionType.Static;
    }
}
