using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashfall.Core
{
    public enum GamePhase { Scavenge60Seconds, ShelterSurvival }

    /// <summary>
    /// Main game brain for phase flow.
    /// Includes hooks for event queues, difficulty scaling, and stress modifiers.
    /// </summary>
    public class GamePhaseController : MonoBehaviour
    {
        [Serializable]
        public class DifficultyProfile
        {
            [Min(1)] public int day = 1;
            [Range(0.5f, 2f)] public float stressMultiplier = 1f;
            [Range(0.7f, 1.3f)] public float scavengingTimeMultiplier = 1f;
        }

        [SerializeField] private float baseScavengingDurationSeconds = 60f;
        [SerializeField] private List<DifficultyProfile> difficultyProfiles = new();

        private readonly Queue<string> _eventQueue = new();
        private float _phaseTimer;
        private float _activeScavengeDuration;

        public int CurrentDay { get; private set; } = 1;
        public float CurrentStressMultiplier { get; private set; } = 1f;
        public GamePhase CurrentPhase { get; private set; }
        public event Action<GamePhase> OnPhaseChanged;
        public event Action<string> OnQueuedEventReady;

        public void Initialize()
        {
            ApplyDifficultyForDay(CurrentDay);
            SetPhase(GamePhase.Scavenge60Seconds);
        }

        private void Update()
        {
            if (CurrentPhase == GamePhase.Scavenge60Seconds)
            {
                _phaseTimer -= Time.deltaTime;
                if (_phaseTimer <= 0f)
                    SetPhase(GamePhase.ShelterSurvival);
            }

            TryDispatchQueuedEvent();
        }

        public void AdvanceDay()
        {
            CurrentDay++;
            ApplyDifficultyForDay(CurrentDay);
            SetPhase(GamePhase.Scavenge60Seconds);
        }

        public void QueueEvent(string eventId)
        {
            if (string.IsNullOrWhiteSpace(eventId)) return;
            _eventQueue.Enqueue(eventId);
        }

        public float GetRemainingPhaseTime() => Mathf.Max(0f, _phaseTimer);

        public void SetPhase(GamePhase phase)
        {
            CurrentPhase = phase;
            _phaseTimer = phase == GamePhase.Scavenge60Seconds ? _activeScavengeDuration : 0f;
            OnPhaseChanged?.Invoke(phase);
        }

        private void ApplyDifficultyForDay(int day)
        {
            DifficultyProfile selected = null;
            foreach (var profile in difficultyProfiles)
            {
                if (profile.day <= day && (selected == null || profile.day > selected.day))
                    selected = profile;
            }

            CurrentStressMultiplier = selected?.stressMultiplier ?? 1f;
            float timeMultiplier = selected?.scavengingTimeMultiplier ?? 1f;
            _activeScavengeDuration = baseScavengingDurationSeconds * timeMultiplier;
        }

        private void TryDispatchQueuedEvent()
        {
            if (_eventQueue.Count == 0 || CurrentPhase != GamePhase.ShelterSurvival) return;
            OnQueuedEventReady?.Invoke(_eventQueue.Dequeue());
        }
    }
}
