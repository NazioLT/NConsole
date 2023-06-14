using System.Reflection;

namespace Nazio_LT.Tools.Console
{
    /// <summary>Describe a command.</summary>
    internal struct CommandContext
    {
        internal CommandContext(MethodInfo method, int argumentCount)
        {
            Error = "";
            Method = method;
            Arguments = new object[argumentCount];
            OtherCommandsPurposes = true;
            Targets = null;
        }

        internal string Error;
        internal bool OtherCommandsPurposes;
        internal MethodInfo Method;
        internal object[] Arguments;
        internal object[] Targets;

        public void SetUniqueTarget(object target)
        {
            Targets = new object[1];
            Targets[0] = target;
        }

        public void Invoke()
        {
            if (Targets == null)
            {
                Method.Invoke(null, Arguments);
                return;
            }

            for (var i = 0; i < Targets.Length; i++)
            {
                Method.Invoke(Targets[i], Arguments);
            }
        }
    }
}
