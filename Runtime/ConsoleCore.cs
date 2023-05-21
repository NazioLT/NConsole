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

        internal static Dictionary<string, MethodInfo> GetAllNCommands()
        {
            Dictionary<string, MethodInfo> ncommands = new Dictionary<string, MethodInfo>();

            Assembly[] assemblies = ConsoleCore.GetLinkedAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (var type in types)
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var method in methods)
                    {
                        if(!method.IsDefined(typeof(NCommandAttribute))) continue;

                        ncommands.Add(method.Name, method);
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

            return result.ToArray();
        }
    }
}
