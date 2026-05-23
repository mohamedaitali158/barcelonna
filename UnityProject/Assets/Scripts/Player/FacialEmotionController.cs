using UnityEngine;

namespace Ashfall.Player
{
    /// <summary>
    /// Drives facial expression blending from stress and panic values.
    /// Bind to Animator parameters / blendshapes for tension storytelling.
    /// </summary>
    public class FacialEmotionController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string stressParam = "Stress";
        [SerializeField] private string panicParam = "Panic";

        [Range(0f, 100f)] public float stress;
        [Range(0f, 100f)] public float panic;

        private void Update()
        {
            if (!animator) return;
            animator.SetFloat(stressParam, stress / 100f);
            animator.SetFloat(panicParam, panic / 100f);
        }

        public void SetFromGameplay(float stressValue, float panicValue)
        {
            stress = Mathf.Clamp(stressValue, 0f, 100f);
            panic = Mathf.Clamp(panicValue, 0f, 100f);
        }
    }
}
