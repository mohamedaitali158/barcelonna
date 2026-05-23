using System;
using UnityEngine;

namespace Ashfall.Gameplay
{
    /// <summary>
    /// 60-second scavenging timer with callbacks for UI and phase transition.
    /// </summary>
    public class ScavengeCountdownTimer : MonoBehaviour
    {
        [SerializeField] private float durationSeconds = 60f;
        public float RemainingSeconds { get; private set; }
        public bool IsRunning { get; private set; }

        public event Action<float> OnTimeChanged;
        public event Action OnTimerFinished;

        public void Begin()
        {
            RemainingSeconds = durationSeconds;
            IsRunning = true;
            OnTimeChanged?.Invoke(RemainingSeconds);
        }

        private void Update()
        {
            if (!IsRunning) return;

            RemainingSeconds -= Time.deltaTime;
            OnTimeChanged?.Invoke(Mathf.Max(RemainingSeconds, 0f));

            if (RemainingSeconds > 0f) return;
            IsRunning = false;
            RemainingSeconds = 0f;
            OnTimerFinished?.Invoke();
        }
    }
}
