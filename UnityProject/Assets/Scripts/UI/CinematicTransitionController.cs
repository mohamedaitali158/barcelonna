using System.Collections;
using Ashfall.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Ashfall.UI
{
    /// <summary>
    /// Handles quick cinematic fades and camera cuts between phases.
    /// </summary>
    public class CinematicTransitionController : MonoBehaviour
    {
        [SerializeField] private Image fadeOverlay;
        [SerializeField] private float fadeDuration = 0.35f;
        [SerializeField] private Camera scavengeCamera;
        [SerializeField] private Camera shelterCamera;

        public void Bind(GamePhaseController phaseController)
        {
            phaseController.OnPhaseChanged += OnPhaseChanged;
        }

        private void OnPhaseChanged(GamePhase phase)
        {
            StartCoroutine(DoPhaseTransition(phase));
        }

        private IEnumerator DoPhaseTransition(GamePhase phase)
        {
            yield return Fade(0f, 1f);

            if (scavengeCamera) scavengeCamera.gameObject.SetActive(phase == GamePhase.Scavenge60Seconds);
            if (shelterCamera) shelterCamera.gameObject.SetActive(phase == GamePhase.ShelterSurvival);

            yield return Fade(1f, 0f);
        }

        private IEnumerator Fade(float from, float to)
        {
            if (!fadeOverlay) yield break;

            float t = 0f;
            Color c = fadeOverlay.color;
            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(from, to, t / fadeDuration);
                fadeOverlay.color = c;
                yield return null;
            }

            c.a = to;
            fadeOverlay.color = c;
        }
    }
}
