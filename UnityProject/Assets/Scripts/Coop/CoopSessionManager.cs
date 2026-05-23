using System;
using System.Threading.Tasks;
using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Coop
{
    public class CoopSessionManager : MonoBehaviour
    {
        [SerializeField] private NetworkSimulationAuthority authority;
        [SerializeField] private CoopPlayerSynchronizer synchronizer;
        [SerializeField] private HiddenGlobalFlags hiddenFlags;

        public event Action OnHostMigrationRequired;
        public event Action<int> OnPlayerDisconnected;

        public async Task StartTwoPlayerSessionAsync()
        {
            // Network transport hookup point (NGO/Mirror/Photon adapter).
            await Task.Yield();
        }

        public bool ValidateSharedInventoryAction(int delta, int current)
        {
            int next = current + delta;
            return next >= 0 && next <= 9999;
        }

        public bool ValidateRareEventTrigger(string eventId)
        {
            return !string.IsNullOrWhiteSpace(eventId) && hiddenFlags.GetCounter($"co_op_trigger_{eventId}") < 1;
        }

        public void MarkRareEventTriggered(string eventId)
        {
            hiddenFlags.Increment($"co_op_trigger_{eventId}");
        }

        public void HandleDisconnect(int playerIndex)
        {
            OnPlayerDisconnected?.Invoke(playerIndex);
            if (authority && authority.IsHost == false)
                OnHostMigrationRequired?.Invoke();
        }
    }
}
