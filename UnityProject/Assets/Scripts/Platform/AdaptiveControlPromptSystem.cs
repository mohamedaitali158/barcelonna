using UnityEngine;
using UnityEngine.InputSystem;

namespace Ashfall.Platform
{
    public class AdaptiveControlPromptSystem : MonoBehaviour
    {
        [SerializeField] private GameObject keyboardPromptRoot;
        [SerializeField] private GameObject xboxPromptRoot;
        [SerializeField] private GameObject playStationPromptRoot;
        [SerializeField] private GameObject touchPromptRoot;

        private void Update()
        {
            var device = InputSystem.GetDevice<Gamepad>();
            bool touch = Touchscreen.current != null && Touchscreen.current.enabled;
            bool hasGamepad = device != null;

            SetActive(keyboardPromptRoot, !hasGamepad && !touch);
            SetActive(touchPromptRoot, touch);

            bool isPs = hasGamepad && device.displayName.ToLower().Contains("dual");
            SetActive(playStationPromptRoot, isPs);
            SetActive(xboxPromptRoot, hasGamepad && !isPs);
        }

        private static void SetActive(GameObject go, bool value)
        {
            if (go && go.activeSelf != value) go.SetActive(value);
        }
    }
}
