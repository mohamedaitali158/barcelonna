using Ashfall.Core;
using UnityEngine;

namespace Ashfall.Systems
{
    /// <summary>
    /// Psychological shelter decay over days: light quality drops, noise rises, humidity pressure rises,
    /// sleep quality drops, and camera tension increases.
    /// </summary>
    public class DynamicShelterDecaySystem : MonoBehaviour
    {
        [Header("Decay State")]
        [Range(0f, 100f)] public float decayLevel;
        [Range(0f, 100f)] public float bunkerNoise;
        [Range(0f, 100f)] public float sleepPenalty;
        [Range(0f, 100f)] public float cameraTension;

        [Header("Rates")]
        [SerializeField] private float baseDailyDecay = 3f;
        [SerializeField] private float fungusContribution = 0.08f;

        [Header("Optional References")]
        [SerializeField] private HumidityFungusSystem fungusSystem;
        [SerializeField] private Light shelterMainLight;

        public void TickDay(GamePhaseController phaseController)
        {
            float fungus = fungusSystem ? fungusSystem.fungusLevel : 0f;
            float dayPressure = Mathf.Clamp01(phaseController.CurrentDay / 40f) * 4f;
            decayLevel = Mathf.Clamp(decayLevel + baseDailyDecay + dayPressure + (fungus * fungusContribution), 0f, 100f);

            bunkerNoise = Mathf.Clamp(decayLevel * 0.85f, 0f, 100f);
            sleepPenalty = Mathf.Clamp(decayLevel * 0.75f, 0f, 100f);
            cameraTension = Mathf.Clamp(decayLevel * 0.9f, 0f, 100f);

            if (shelterMainLight)
            {
                float t = decayLevel / 100f;
                shelterMainLight.intensity = Mathf.Lerp(1.1f, 0.55f, t);
                shelterMainLight.color = Color.Lerp(new Color(1f, 0.96f, 0.9f), new Color(0.82f, 0.9f, 0.78f), t);
            }
        }
    }
}
