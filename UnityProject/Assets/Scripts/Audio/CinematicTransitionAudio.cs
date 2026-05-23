using Ashfall.Core;
using UnityEngine;

namespace Ashfall.Audio
{
    /// <summary>
    /// Plays cinematic transition sounds between key loop beats.
    /// </summary>
    public class CinematicTransitionAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip sirenClip;
        [SerializeField] private AudioClip breathingClip;
        [SerializeField] private AudioClip bunkerDoorClip;

        public void Bind(GamePhaseController phaseController)
        {
            phaseController.OnPhaseChanged += HandlePhaseChanged;
        }

        private void HandlePhaseChanged(GamePhase phase)
        {
            if (phase == GamePhase.Scavenge60Seconds)
            {
                Play(sirenClip);
                Play(breathingClip);
            }
            else
            {
                Play(bunkerDoorClip);
            }
        }

        public void PlayExpeditionReturn() => Play(bunkerDoorClip);

        private void Play(AudioClip clip)
        {
            if (!sfxSource || !clip) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}
