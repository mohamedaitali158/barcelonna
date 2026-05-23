using System;
using Ashfall.Core;
using Ashfall.Save;
using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Resume
{
    public class CheckpointManager : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private DynamicShelterDecaySystem shelterDecay;

        public RuntimeStateSnapshot BuildSnapshot(string expeditionState, string activeEventId)
        {
            return new RuntimeStateSnapshot
            {
                saveData = saveSystem.Current,
                phase = phaseController.CurrentPhase,
                phaseTimeRemaining = phaseController.GetRemainingPhaseTime(),
                expeditionState = expeditionState,
                activeEventId = activeEventId,
                shelterDecay = shelterDecay ? shelterDecay.decayLevel : 0f,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds()
            };
        }
    }
}
