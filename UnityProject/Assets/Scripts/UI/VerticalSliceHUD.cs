using Ashfall.Core;
using Ashfall.Gameplay;
using TMPro;
using UnityEngine;

namespace Ashfall.UI
{
    /// <summary>
    /// Minimal HUD for playable day loop.
    /// </summary>
    public class VerticalSliceHUD : MonoBehaviour
    {
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private ScavengeCountdownTimer timer;
        [SerializeField] private TextMeshProUGUI phaseLabel;
        [SerializeField] private TextMeshProUGUI timerLabel;
        [SerializeField] private TextMeshProUGUI dayLabel;

        private void Start()
        {
            phaseController.OnPhaseChanged += UpdatePhase;
            timer.OnTimeChanged += UpdateTimer;

            UpdatePhase(phaseController.CurrentPhase);
            UpdateTimer(timer.RemainingSeconds);
            dayLabel.text = $"Day {phaseController.CurrentDay}";
        }

        private void OnDestroy()
        {
            phaseController.OnPhaseChanged -= UpdatePhase;
            timer.OnTimeChanged -= UpdateTimer;
        }

        private void UpdatePhase(GamePhase phase)
        {
            phaseLabel.text = phase == GamePhase.Scavenge60Seconds ? "Scavenge" : "Shelter";
            dayLabel.text = $"Day {phaseController.CurrentDay}";
        }

        private void UpdateTimer(float seconds)
        {
            timerLabel.text = Mathf.CeilToInt(seconds).ToString();
        }
    }
}
