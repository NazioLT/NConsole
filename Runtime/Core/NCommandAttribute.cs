using System;

namespace Nazio_LT.Tools.Console
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NCommandAttribute : Attribute
    {
        public string Description = "";
        public string CustomName = "";
        public bool UseSelectedObject = false;
    }
}
