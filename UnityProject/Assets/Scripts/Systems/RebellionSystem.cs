using UnityEngine;

namespace Ashfall.Systems
{
    public class RebellionSystem : MonoBehaviour
    {
        [SerializeField] private FamilyStressSystem familyStress;
        [SerializeField] private float baseChance = 0.02f; // 2%

        public bool RollRebellion(float trustLevel, float foodDaysRemaining)
        {
            float stressFactor = familyStress.HouseholdAverage() / 100f;
            float starvationFactor = Mathf.Clamp01(1f - (foodDaysRemaining / 7f));
            float trustPenalty = 1f - trustLevel;

            float chance = baseChance + (0.07f * stressFactor) + (0.05f * starvationFactor) + (0.03f * trustPenalty);
            return Random.value < chance;
        }
    }
}
