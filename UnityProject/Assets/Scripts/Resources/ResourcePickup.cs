using Ashfall.Data;
using Ashfall.Inventory;
using UnityEngine;

namespace Ashfall.Resources
{
    /// <summary>
    /// Attach to pickup objects to collect resource items.
    /// </summary>
    public class ResourcePickup : MonoBehaviour
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private int amount = 1;

        private void OnTriggerEnter(Collider other)
        {
            var inventory = other.GetComponent<PlayerInventory>();
            if (!inventory) return;

            inventory.Add(itemType, amount);
            Destroy(gameObject);
        }
    }
}
