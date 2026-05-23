using Ashfall.Save;
using UnityEngine;

namespace Ashfall.Networking
{
    /// <summary>
    /// Boundary layer for future server-authoritative simulation.
    /// Current single-player mode routes locally while preserving interface split.
    /// </summary>
    public class AuthoritativeSimulationBridge : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;

        public SaveData GetAuthoritativeState() => saveSystem.Current;

        public void ApplyAuthoritativeState(SaveData authoritative)
        {
            saveSystem.OverrideCurrent(authoritative);
            saveSystem.SaveSync();
        }
    }
}
