using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal static class ConsoleCore
    {
        public static LogInfos MessageTypeFactory(LogType type)
        {
            switch (type)
            {
                case LogType.Warning:
                    return WarningMessageInfos;

                case LogType.Error:
                    return ErrorMessageInfos;

                case LogType.Exception:
                    return ExceptionMessageInfos;

                case LogType.Assert:
                    return AssertMessageInfos;
            }

            return LogMessageInfos;
        }

        public static readonly LogInfos LogMessageInfos = new LogInfos(
            Color.white,
            "Log"
        );

        public static readonly LogInfos WarningMessageInfos = new LogInfos(
            Color.yellow,
            "Warning"
        );

        public static readonly LogInfos ErrorMessageInfos = new LogInfos(
            Color.red,
            "Error"
        );

        public static readonly LogInfos ExceptionMessageInfos = new LogInfos(
            Color.red,
            "Exception"
        );

        public static readonly LogInfos AssertMessageInfos = new LogInfos(
            Color.red,
            "Assert"
        );

        internal static Dictionary<string, NCommand> GetAllNCommands()
        {
            Dictionary<string, NCommand> ncommands = new Dictionary<string, NCommand>();

            Assembly[] assemblies = ConsoleCore.GetLinkedAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (var type in types)
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var method in methods)
                    {
                        if (!method.IsDefined(typeof(NCommandAttribute))) continue;

                        NCommandAttribute attribute = method.GetCustomAttribute<NCommandAttribute>();

                        NCommand command = new NCommand {
                            Attribute = attribute,
                            Name = attribute.CustomName == "" ? method.Name : attribute.CustomName,
                            Method = method
                        };  

                        ncommands.Add(command.Name, command);
                    }
                }
            }

            return ncommands;
        }

        internal static Assembly[] GetLinkedAssemblies()
        {
            List<Assembly> result = new List<Assembly>();

            string currentAssemblyName = Assembly.GetCallingAssembly().FullName;
            Assembly[] allAssemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in allAssemblies)
            {
                foreach (var assemblyRef in assembly.GetReferencedAssemblies())
                {
                    if (assemblyRef.FullName == currentAssemblyName)
                    {
                        result.Add(assembly);
                        break;
                    }
                }
            }

            result.Add(Assembly.GetExecutingAssembly());

            return result.ToArray();
        }

        internal static bool IsArgumentValid(ParameterInfo parameter, string token, out object value)
        {
            Type argumentType = parameter.ParameterType;

            if (argumentType == typeof(string))
            {
                value = token;

                return true;
            }

            if (argumentType == typeof(int))
            {
                bool valid = int.TryParse(token, out int output);
                value = output;

                return valid;
            }

            if (argumentType == typeof(float))
            {
                bool valid = float.TryParse(token, out float output);
                value = output;

                return valid;
            }

            if (argumentType == typeof(bool))
            {
                token = token.ToLower();
                value = token == "true";

                return token == "false" || token == "true";
            }

            value = null;
            return false;
        }
    }
}
