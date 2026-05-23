using System;
using UnityEngine;

namespace Ashfall.Security
{
    public class TamperDetectionSystem : MonoBehaviour
    {
        [SerializeField] private IntegrityValidationSystem integrity;
        public event Action<string> OnTamperDetected;

        private void Awake()
        {
            if (integrity) integrity.OnSuspiciousDetected += msg => OnTamperDetected?.Invoke(msg);
        }

        public bool DetectCheatEngineBasics()
        {
            // Basic heuristic placeholder without unsupported APIs.
            float scale = Time.timeScale;
            if (scale > 2f)
            {
                OnTamperDetected?.Invoke($"Suspicious timeScale: {scale}");
                return true;
            }
            return false;
        }
    }
}
