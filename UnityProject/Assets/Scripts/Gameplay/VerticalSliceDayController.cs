using Ashfall.Core;
using Ashfall.Inventory;
using Ashfall.Systems;
using Ashfall.Save;
using UnityEngine;

namespace Ashfall.Gameplay
{
    /// <summary>
    /// Playable vertical slice loop: Scavenge -> Shelter -> Next day.
    /// Use with one house, one survivor, and one HUD.
    /// </summary>
    public class VerticalSliceDayController : MonoBehaviour
    {
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private ScavengeCountdownTimer timer;
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private FamilyStressSystem stressSystem;
        [SerializeField] private HumidityFungusSystem fungusSystem;
        [SerializeField] private DynamicShelterDecaySystem shelterDecaySystem;
        [SerializeField] private SaveSystem saveSystem;
        [SerializeField] private CharacterBehavioralDecaySystem behavioralDecaySystem;
        [SerializeField] private RareEndingResolver rareEndingResolver;

        [SerializeField] private string survivorId = "Survivor_A";
        [SerializeField] private bool ventilationDamaged;

        private void Start()
        {
            stressSystem.RegisterMember(survivorId);
            behavioralDecaySystem.Initialize();

            timer.OnTimerFinished += EnterShelterPhase;
            phaseController.OnPhaseChanged += OnPhaseChanged;
            phaseController.Initialize();
            timer.Begin();
        }

        private void OnDestroy()
        {
            timer.OnTimerFinished -= EnterShelterPhase;
            phaseController.OnPhaseChanged -= OnPhaseChanged;
        }

        private void OnPhaseChanged(GamePhase phase)
        {
            if (phase == GamePhase.Scavenge60Seconds)
                timer.Begin();
        }

        private void EnterShelterPhase()
        {
            phaseController.SetPhase(GamePhase.ShelterSurvival);

            // Day-end simulation hooks.
            fungusSystem.TickDay(ventilationDamaged);
            stressSystem.ModifyStress(survivorId, 8f * phaseController.CurrentStressMultiplier);

            shelterDecaySystem.TickDay(phaseController);
            behavioralDecaySystem.TickDay(phaseController);

            // Queue one sample event for shelter processing.
            phaseController.QueueEvent("daily_status_report");
            rareEndingResolver.EvaluateAtLateGameCheckpoint();
        }

        public void EndShelterAndStartNextDay()
        {
            saveSystem.SaveAsync(this);
            phaseController.AdvanceDay();
        }
    }
}
