using System;
using UnityEngine;

namespace Ashfall.Security
{
    public class SecurityManager : MonoBehaviour
    {
        [SerializeField] private RuntimeIntegritySystem runtimeIntegrity;
        [SerializeField] private TamperDetectionSystem tamperDetection;
        [SerializeField] private SecureSaveValidator saveValidator;

        public string DeviceFingerprint { get; private set; }
        public bool OfflineSecureMode { get; private set; }

        private void Awake()
        {
            DeviceFingerprint = $"{SystemInfo.deviceUniqueIdentifier}_{SystemInfo.operatingSystem}_{SystemInfo.graphicsDeviceName}";
            OfflineSecureMode = Application.internetReachability == NetworkReachability.NotReachable;

            if (runtimeIntegrity)
            {
                runtimeIntegrity.OnIntegrityIssue += LogSecurity;
                runtimeIntegrity.ValidateAssemblyHash();
            }

            if (tamperDetection)
                tamperDetection.OnTamperDetected += LogSecurity;

            if (saveValidator)
                saveValidator.OnSecurityAlert += LogSecurity;
        }

        private static void LogSecurity(string message)
        {
            Debug.LogWarning($"[SecurityManager] {message}");
        }
    }
}
