using Ashfall.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace Ashfall.Audio
{
    public class DynamicMusicController : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot scavenge;
        [SerializeField] private AudioMixerSnapshot shelter;

        public void Initialize(GamePhaseController phaseController)
        {
            phaseController.OnPhaseChanged += HandlePhaseChanged;
            HandlePhaseChanged(phaseController.CurrentPhase);
        }

        private void HandlePhaseChanged(GamePhase phase)
        {
            (phase == GamePhase.Scavenge60Seconds ? scavenge : shelter).TransitionTo(1.2f);
        }
    }
}
