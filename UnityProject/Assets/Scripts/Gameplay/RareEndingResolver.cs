using System;
using Ashfall.Core;
using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Gameplay
{
    public enum RealityFractureEnding
    {
        None,
        WallBreathing,
        FalseRescue,
        LaughingShelter,
        Observer
    }

    /// <summary>
    /// Evaluates very high-complexity hidden ending chains over long-term play.
    /// Not pure RNG: requires compounded behavior + world-state constraints.
    /// </summary>
    public class RareEndingResolver : MonoBehaviour
    {
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private HumidityFungusSystem fungusSystem;
        [SerializeField] private FamilyStressSystem stressSystem;
        [SerializeField] private HiddenGlobalFlags hiddenFlags;

        [Tooltip("Final microscopic trigger chance after all condition chains pass.")]
        [SerializeField] private float finalFractureChance = 0.000001f; // 0.0001%

        public event Action<RealityFractureEnding> OnEndingResolved;

        public void EvaluateAtLateGameCheckpoint()
        {
            if (phaseController.CurrentDay < 40) return;

            bool chainWallBreathing =
                hiddenFlags.Has("never_used_gun") &&
                hiddenFlags.Has("trusted_rebel") &&
                hiddenFlags.Has("radio_intact") &&
                hiddenFlags.GetCounter("wall_noise_count") >= 7 &&
                hiddenFlags.GetCounter("ignored_radio_signal") >= 3 &&
                hiddenFlags.GetCounter("shadow_event_night") >= 1 &&
                fungusSystem.fungusLevel >= 66f &&
                stressSystem.HouseholdAverage() >= 70f;

            bool chainFalseRescue =
                hiddenFlags.Has("saw_shadow_event") &&
                hiddenFlags.Has("refused_last_rescue") &&
                hiddenFlags.GetCounter("silent_days") >= 5 &&
                hiddenFlags.GetCounter("night_dialogue_choice_7") >= 1 &&
                phaseController.CurrentDay >= 52;

            bool chainLaughingShelter =
                hiddenFlags.GetCounter("nervous_laughter_spike_days") >= 9 &&
                hiddenFlags.Has("laughed_during_death") &&
                hiddenFlags.GetCounter("power_outage_nights") >= 4 &&
                fungusSystem.fungusLevel >= 72f;

            bool chainObserver =
                hiddenFlags.GetCounter("camera_stare_same_anchor") >= 144 &&
                hiddenFlags.Has("observer_dialogue_seed") &&
                hiddenFlags.GetCounter("mirror_checks") >= 11 &&
                hiddenFlags.GetCounter("no_music_days") >= 6;

            // Hidden microscopic gate after chain completion (legendary rarity).
            if (UnityEngine.Random.value > finalFractureChance) return;

            if (chainWallBreathing) Resolve(RealityFractureEnding.WallBreathing);
            else if (chainFalseRescue) Resolve(RealityFractureEnding.FalseRescue);
            else if (chainLaughingShelter) Resolve(RealityFractureEnding.LaughingShelter);
            else if (chainObserver) Resolve(RealityFractureEnding.Observer);
        }

        private void Resolve(RealityFractureEnding ending)
        {
            OnEndingResolved?.Invoke(ending);
        }
    }
}
