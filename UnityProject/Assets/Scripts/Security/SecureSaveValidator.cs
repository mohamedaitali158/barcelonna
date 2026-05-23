using System;
using UnityEngine;

namespace Ashfall.Security
{
    public class SecureSaveValidator : MonoBehaviour
    {
        [SerializeField] private RuntimeIntegritySystem runtimeIntegrity;
        [SerializeField] private TamperDetectionSystem tamperDetection;

        public event Action<string> OnSecurityAlert;

        public bool RunPreSaveChecks()
        {
            bool ok1 = runtimeIntegrity == null || runtimeIntegrity.ValidateAssemblyHash();
            bool ok2 = runtimeIntegrity == null || runtimeIntegrity.DetectDebugger();
            bool ok3 = tamperDetection == null || !tamperDetection.DetectCheatEngineBasics();
            if (!(ok1 && ok2 && ok3))
            {
                OnSecurityAlert?.Invoke("Security pre-save checks failed.");
                return false;
            }
            return true;
        }
    }
}
