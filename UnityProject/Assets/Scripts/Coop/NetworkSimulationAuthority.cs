using Ashfall.Save;
using UnityEngine;

namespace Ashfall.Coop
{
    /// <summary>
    /// Host-authoritative state gate for co-op simulation writes.
    /// </summary>
    public class NetworkSimulationAuthority : MonoBehaviour
    {
        [SerializeField] private bool isHost;
        [SerializeField] private SaveSystem saveSystem;

        public bool IsHost => isHost;

        public bool TryApplyAuthoritativeSave(SaveData incoming)
        {
            if (!isHost || incoming == null) return false;
            saveSystem.OverrideCurrent(incoming);
            return true;
        }

        public SaveData ExportAuthoritativeState() => saveSystem.Current;
    }
}
