using System;
using UnityEngine;

namespace Ashfall.Core
{
    public enum GamePhase { Scavenge60Seconds, ShelterSurvival }

    /// <summary>
    /// Manages transition between real-time scavenging and shelter management.
    /// </summary>
    public class GamePhaseController : MonoBehaviour
    {
        [SerializeField] private float scavengingDurationSeconds = 60f;
        private float phaseTimer;

        public GamePhase CurrentPhase { get; private set; }
        public event Action<GamePhase> OnPhaseChanged;

        public void Initialize()
        {
            SetPhase(GamePhase.Scavenge60Seconds);
        }

        private void Update()
        {
            if (CurrentPhase != GamePhase.Scavenge60Seconds) return;
            phaseTimer -= Time.deltaTime;
            if (phaseTimer <= 0f)
            {
                SetPhase(GamePhase.ShelterSurvival);
            }
        }

        public void SetPhase(GamePhase phase)
        {
            CurrentPhase = phase;
            phaseTimer = phase == GamePhase.Scavenge60Seconds ? scavengingDurationSeconds : 0f;
            OnPhaseChanged?.Invoke(phase);
        }
    }
}
