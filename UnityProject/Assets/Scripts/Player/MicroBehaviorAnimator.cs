using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Player
{
    /// <summary>
    /// Drives subtle psychological micro-animations from behavioral decay state.
    /// </summary>
    public class MicroBehaviorAnimator : MonoBehaviour
    {
        [SerializeField] private string characterId = "Survivor_A";
        [SerializeField] private CharacterBehavioralDecaySystem behavioralDecay;
        [SerializeField] private Animator animator;

        [Header("Animator Parameters")]
        [SerializeField] private string touchFaceParam = "TouchFace";
        [SerializeField] private string lookBackParam = "LookBack";
        [SerializeField] private string handTremorParam = "HandTremor";
        [SerializeField] private string fastBreathParam = "FastBreath";
        [SerializeField] private string footFidgetParam = "FootFidget";
        [SerializeField] private string longStareParam = "LongStare";

        private void Update()
        {
            if (!animator || !behavioralDecay) return;

            var state = behavioralDecay.GetState(characterId);
            if (state == null) return;

            float anxiety = Mathf.Clamp01((1f - state.socialTrust) * 0.45f + state.isolationNeed * 0.35f + state.nervousLaughter * 0.2f);
            animator.SetFloat(touchFaceParam, Mathf.Clamp01(anxiety * 0.9f));
            animator.SetFloat(lookBackParam, Mathf.Clamp01(anxiety * 0.7f));
            animator.SetFloat(handTremorParam, Mathf.Clamp01(anxiety * 1.1f));
            animator.SetFloat(fastBreathParam, Mathf.Clamp01(anxiety));
            animator.SetFloat(footFidgetParam, Mathf.Clamp01(anxiety * 0.8f));
            animator.SetFloat(longStareParam, Mathf.Clamp01(state.isolationNeed));
        }
    }
}
