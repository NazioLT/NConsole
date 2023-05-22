using System.Reflection;

namespace Nazio_LT.Tools.Console
{
    internal struct CommandContext
    {
        internal CommandContext(MethodInfo method, int argumentCount)
        {
            Error = "";
            Method = method;
            Arguments = new object[argumentCount];
        }

        internal string Error;
        internal MethodInfo Method;
        internal object[] Arguments;

        public void Invoke()
        {
            Method.Invoke(null, Arguments);
        }
    }
}
