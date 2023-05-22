using System.Reflection;

namespace Nazio_LT.Tools.Console
{
    internal struct NCommand
    {
        public string Name;
        public NCommandAttribute Attribute;
        public MethodInfo Method;
        public ParameterInfo[] ParameterInfos;

        public string Description
        {
            get
            {
                string arguments = " ";

                foreach (var param in ParameterInfos)
                {
                    arguments += $"({param.ParameterType.ToString()}){param.Name} ";
                }

                string commandText = Name + arguments;
                string description = Attribute.Description == "" ? "" : (" : " + Attribute.Description);

                return commandText + description;
            }
        }
    }
}
