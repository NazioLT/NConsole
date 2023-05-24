using System.Collections.Generic;
using UnityEngine;

namespace Nazio_LT.Tools.Console
{
    public static class BasicCommands
    {
        [NCommand(Description = "Display all available commands.")]
        public static void Help()
        {
            NConsole console = NConsole.Instance;

            if (console == null)
            {
                throw new System.Exception("No console instance");
            }

            Dictionary<string, NCommandPolymorphism> commands = console.Ncommands;
            string helpers = "List of all available commands : " + '\n';

            foreach (var cmd in commands.Values)
            {
                helpers += cmd.ToString() + '\n';
            }

            Debug.Log(helpers);
        }

        [NCommand(Description = "Clear the console.")]
        public static void Clear() => NConsole.Instance.Clear();

        [NCommand]
        public static void GetSceneHierarchy()
        {
            Debug.LogWarning("Not Implemented");
        }

        [NCommand]
        public static void Select(string objectName)
        {
            GameObject obj = GameObject.Find(objectName);

            if (obj == null)
            {
                Debug.LogWarning($"Object {objectName} was not found.");
                return;
            }

            NConsole.Instance.SetSelectedGameObject(obj);
            Debug.Log($"Object {ConsoleCore.GetGameObjectPath(obj)} was Found.");
        }

        [NCommand(UseSelectedObject = true)]
        public static void Selected(GameObject selectedObject)
        {
            Debug.Log($"Selected object is : {ConsoleCore.GetGameObjectPath(selectedObject)}.");
        }

        [NCommand(UseSelectedObject = true)]
        public static void Destroy(GameObject selectedObject)
        {
            Debug.Log($"Destroyed : {ConsoleCore.GetGameObjectPath(selectedObject)}.");
            GameObject.Destroy(selectedObject);
        }

        [NCommand(UseSelectedObject = true)]
        public static void Move(GameObject selectedObject, Vector3 position)
        {
            Debug.Log($"{ConsoleCore.GetGameObjectPath(selectedObject)} moved to : {position}.");
            selectedObject.transform.position = position;
        }
    }
}
