using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Nazio_LT.Tools.Console
{
    public class Terminal : TMPro.TMP_InputField
    {
        public override void OnUpdateSelected(BaseEventData eventData)
        {
            base.OnUpdateSelected(eventData);

#if ENABLE_INPUT_SYSTEM

            if (Keyboard.current[Key.UpArrow].wasPressedThisFrame)
            {
                // Debug.Log("UP");
                NConsole.Instance.ArrowInput(true);
            }

            if (Keyboard.current[Key.DownArrow].wasPressedThisFrame)
            {
                // Debug.Log("DOWN");
                NConsole.Instance.ArrowInput(false);
            }

            if (Keyboard.current[Key.Tab].wasPressedThisFrame)
            {
                NConsole.Instance.Tab();
            }

#else

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NConsole.Instance.ArrowInput(true);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NConsole.Instance.ArrowInput(false);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                NConsole.Instance.Tab();
            }

#endif
        }
    }
}