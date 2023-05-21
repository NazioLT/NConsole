using System;

namespace Nazio_LT.Tools.Console
{
    [AttributeUsage(AttributeTargets.Method)]
    public class NCommandAttribute : Attribute
    {
        public NCommandAttribute()
        {
        }
    }
}
