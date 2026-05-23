using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ashfall.Input
{
    public class DynamicPromptUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI promptLabel;
        [SerializeField] private string keyboardText = "Press [E]";
        [SerializeField] private string xboxText = "Press [X]";
        [SerializeField] private string playStationText = "Press [□]";
        [SerializeField] private string touchText = "Tap";

        private void Update()
        {
            if (!promptLabel) return;

            if (Touchscreen.current != null && Touchscreen.current.enabled) { promptLabel.text = touchText; return; }
            if (Gamepad.current != null)
            {
                bool ps = Gamepad.current.displayName.ToLower().Contains("dual");
                promptLabel.text = ps ? playStationText : xboxText;
                return;
            }

            promptLabel.text = keyboardText;
        }
    }
}
