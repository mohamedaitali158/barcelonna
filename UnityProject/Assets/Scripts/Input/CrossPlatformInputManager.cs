using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ashfall.Input
{
    /// <summary>
    /// Detects active device and toggles touch UI roots.
    /// </summary>
    public class CrossPlatformInputManager : MonoBehaviour
    {
        [SerializeField] private GameObject mobileTouchUiRoot;
        public event Action<string> OnInputDeviceChanged;

        private string _last;

        private void Update()
        {
            string current = DetectCurrentDevice();
            if (current != _last)
            {
                _last = current;
                OnInputDeviceChanged?.Invoke(current);
            }

            if (mobileTouchUiRoot)
                mobileTouchUiRoot.SetActive(current == "Touch");
        }

        private static string DetectCurrentDevice()
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed) return "Touch";
            if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame) return Gamepad.current.displayName;
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) return "KeyboardMouse";
            return "Unknown";
        }
    }
}
