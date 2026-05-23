using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Audio
{
    /// <summary>
    /// Uses silence as a horror tool: lowers music bed as talkativeness drops and promotes breathing/room tone.
    /// </summary>
    public class SilenceTensionAudioController : MonoBehaviour
    {
        [SerializeField] private CharacterBehavioralDecaySystem behavioralDecay;
        [SerializeField] private string characterId = "Survivor_A";

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicBed;
        [SerializeField] private AudioSource breathingLoop;
        [SerializeField] private AudioSource bunkerAmbience;
        [SerializeField] private AudioSource waterDripLoop;
        [SerializeField] private AudioSource nervousLaughStinger;

        [Header("Mix Targets")]
        [SerializeField] private float musicMin = 0.05f;
        [SerializeField] private float musicMax = 0.75f;
        [SerializeField] private float ambienceMax = 0.7f;
        [SerializeField] private float breatheMax = 0.8f;

        private float _laughCooldown;

        private void Update()
        {
            if (!behavioralDecay) return;
            var state = behavioralDecay.GetState(characterId);
            if (state == null) return;

            float silencePressure = Mathf.Clamp01(1f - state.talkativeness);

            if (musicBed) musicBed.volume = Mathf.Lerp(musicMax, musicMin, silencePressure);
            if (bunkerAmbience) bunkerAmbience.volume = Mathf.Lerp(0.25f, ambienceMax, silencePressure);
            if (breathingLoop) breathingLoop.volume = Mathf.Lerp(0.1f, breatheMax, silencePressure);
            if (waterDripLoop) waterDripLoop.volume = Mathf.Lerp(0.05f, 0.4f, silencePressure);

            _laughCooldown -= Time.deltaTime;
            if (state.nervousLaughter > 0.75f && _laughCooldown <= 0f && nervousLaughStinger)
            {
                nervousLaughStinger.Play();
                _laughCooldown = Random.Range(8f, 16f);
            }
        }
    }
}
