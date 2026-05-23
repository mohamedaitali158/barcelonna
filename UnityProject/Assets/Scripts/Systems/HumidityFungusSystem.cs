using Ashfall.Data;
using UnityEngine;

namespace Ashfall.Systems
{
    /// <summary>
    /// Simulates shelter dampness and toxic fungus spread.
    /// </summary>
    public class HumidityFungusSystem : MonoBehaviour
    {
        [Range(0f, 100f)] public float humidity = 40f;
        [Range(0f, 100f)] public float fungusLevel = 0f;

        [SerializeField] private InventorySystem inventory;

        public void TickDay(bool ventilationDamaged)
        {
            humidity += ventilationDamaged ? 12f : 5f;
            humidity = Mathf.Clamp(humidity, 0f, 100f);

            if (humidity > 65f) fungusLevel += (humidity - 60f) * 0.1f;
            fungusLevel = Mathf.Clamp(fungusLevel, 0f, 100f);
        }

        public bool CleanShelter()
        {
            if (!inventory.TryConsume(ItemType.Cleaners, 1)) return false;
            fungusLevel = Mathf.Max(0f, fungusLevel - 18f);
            humidity = Mathf.Max(0f, humidity - 10f);
            return true;
        }
    }
}
