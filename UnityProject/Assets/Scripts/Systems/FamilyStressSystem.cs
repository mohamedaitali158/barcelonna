using System.Collections.Generic;
using UnityEngine;

namespace Ashfall.Systems
{
    public class FamilyStressSystem : MonoBehaviour
    {
        private readonly Dictionary<string, float> _stress = new();

        public void RegisterMember(string memberId) => _stress.TryAdd(memberId, 25f);

        public void ModifyStress(string memberId, float delta)
        {
            if (!_stress.ContainsKey(memberId)) return;
            _stress[memberId] = Mathf.Clamp(_stress[memberId] + delta, 0f, 100f);
        }

        public float HouseholdAverage()
        {
            if (_stress.Count == 0) return 0f;
            float total = 0f;
            foreach (var kvp in _stress) total += kvp.Value;
            return total / _stress.Count;
        }
    }
}
