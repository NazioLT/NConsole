using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Nazio_LT.Tools.Console
{
    public class Terminal : TMPro.TMP_InputField
    {
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);

            // eventData.currentInputModule

#if ENABLE_INPUT_SYSTEM

            Debug.Log("New Input System");

#else

            // Debug.Log("Old Input System");

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // Debug.Log("UP");
                NConsole.Instance.ArrowInput(true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // Debug.Log("DOWN");
                NConsole.Instance.ArrowInput(false);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log("Tab");
            }

#endif
        }
    }
}