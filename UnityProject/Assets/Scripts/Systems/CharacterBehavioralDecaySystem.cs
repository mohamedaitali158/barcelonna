using System;
using System.Collections.Generic;
using Ashfall.Core;
using UnityEngine;

namespace Ashfall.Systems
{
    [Serializable]
    public class BehavioralState
    {
        public string characterId;
        [Range(0f, 1f)] public float talkativeness = 1f;
        [Range(0f, 1f)] public float socialTrust = 1f;
        [Range(0f, 1f)] public float movementSpeedFactor = 1f;
        [Range(0f, 1f)] public float obedience = 1f;
        [Range(0f, 1f)] public float isolationNeed;
        [Range(0f, 1f)] public float nervousLaughter;
    }

    /// <summary>
    /// Psychological character degradation over days.
    /// Drives social silence, fear, slowed motion, isolation, disobedience, and odd laughter spikes.
    /// </summary>
    public class CharacterBehavioralDecaySystem : MonoBehaviour
    {
        [SerializeField] private DynamicShelterDecaySystem shelterDecay;
        [SerializeField] private FamilyStressSystem familyStress;
        [SerializeField] private List<string> trackedCharacters = new() { "Survivor_A" };

        private readonly Dictionary<string, BehavioralState> _states = new();
        public event Action<BehavioralState> OnBehaviorChanged;

        public void Initialize()
        {
            foreach (var id in trackedCharacters)
            {
                if (!_states.ContainsKey(id))
                    _states[id] = new BehavioralState { characterId = id };
            }
        }

        public void TickDay(GamePhaseController phaseController)
        {
            float shelterPressure = shelterDecay ? shelterDecay.decayLevel / 100f : 0f;
            float stressPressure = familyStress ? familyStress.HouseholdAverage() / 100f : 0f;
            float dayPressure = Mathf.Clamp01(phaseController.CurrentDay / 40f);
            float fear = Mathf.Clamp01((shelterPressure * 0.45f) + (stressPressure * 0.4f) + (dayPressure * 0.15f));

            foreach (var key in new List<string>(_states.Keys))
            {
                var state = _states[key];
                state.talkativeness = Mathf.Clamp01(1f - (fear * 0.85f));
                state.socialTrust = Mathf.Clamp01(1f - (fear * 0.7f));
                state.movementSpeedFactor = Mathf.Clamp(1f - (fear * 0.35f), 0.55f, 1f);
                state.obedience = Mathf.Clamp01(1f - (fear * 0.65f));
                state.isolationNeed = Mathf.Clamp01(fear * 0.9f);

                // Occasional uncanny laughter spikes under high fear.
                float spike = fear > 0.6f ? UnityEngine.Random.Range(0f, 0.35f) : 0f;
                state.nervousLaughter = Mathf.Clamp01((fear * 0.2f) + spike);

                _states[key] = state;
                OnBehaviorChanged?.Invoke(state);
            }
        }

        public BehavioralState GetState(string characterId)
        {
            return _states.TryGetValue(characterId, out var state) ? state : null;
        }
    }
}
