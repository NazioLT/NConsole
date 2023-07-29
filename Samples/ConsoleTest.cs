using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nazio_LT.Tools.Console;

namespace Nazio_LT.Tools.Console.Test
{
    public class ConsoleTest : MonoBehaviour
    {
        [SerializeField] private string _testText = "";

        [NCommand(ExecutionMode = ExecutionType.SelectedObjectInstance)]
        public void TestConsole()
        {
            Debug.Log("Test Succed : " + _testText);
        }
    }
}