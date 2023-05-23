using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    internal static class ConsoleCore
    {
        internal static Dictionary<string, NCommandPolymorphism> GetAllNCommands()
        {
            Dictionary<string, NCommandPolymorphism> ncommands = new Dictionary<string, NCommandPolymorphism>();

            Assembly[] assemblies = ConsoleCore.GetLinkedAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (var type in types)
                {
                    MethodInfo[] methods = type.GetMethods(BindingFlags.Static | BindingFlags.Public);
                    foreach (var method in methods)
                    {
                        if (!method.IsDefined(typeof(NCommandAttribute))) continue;

                        NCommandAttribute attribute = method.GetCustomAttribute<NCommandAttribute>();

                        ParameterInfo[] parameterInfos = method.GetParameters();

                        NCommand command = new NCommand
                        {
                            Attribute = attribute,
                            Name = attribute.CustomName == "" ? method.Name : attribute.CustomName,
                            Method = method,
                            ParameterInfos = parameterInfos
                        };

                        string key = command.Name;

                        if (ncommands.ContainsKey(key))
                        {
                            ncommands[key].Add(command);
                            continue;
                        }

                        ncommands.Add(key, new NCommandPolymorphism(command));
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

        public static string GetGameObjectPath(GameObject obj)
        {
            Transform transform = obj.transform;
            string path = obj.transform.name;
            while (transform.parent != null)
            {
                path = $"/{transform.parent.name}{path}";
                transform = transform.parent;
            }
            return path;
        }
    }
}
