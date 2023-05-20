using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Nazio_LT.Tools.Console
{
    public class NConsole : MonoBehaviour
    {
        private static NConsole s_instance = null;

        [SerializeField] private Transform m_consoleContentParent = null;
        [SerializeField] private TextMeshProUGUI m_textPrefab = null;

        private void Awake()
        {
            if(s_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = this;
        }

        public void Print(string message)
        {
            TextMeshProUGUI text = Instantiate(m_textPrefab, transform.position, Quaternion.identity, m_consoleContentParent);
            text.text = message;
        }

        public static NConsole Instance => s_instance;
    }
}
