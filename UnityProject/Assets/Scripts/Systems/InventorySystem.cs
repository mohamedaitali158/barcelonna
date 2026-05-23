using System.Collections.Generic;
using Ashfall.Data;
using UnityEngine;

namespace Ashfall.Systems
{
    /// <summary>
    /// Central inventory with stack handling for shelter and expedition loadouts.
    /// </summary>
    public class InventorySystem : MonoBehaviour
    {
        private readonly Dictionary<ItemType, int> _items = new();

        public int GetCount(ItemType type) => _items.TryGetValue(type, out var count) ? count : 0;

        public void Add(ItemType type, int amount)
        {
            if (!_items.ContainsKey(type)) _items[type] = 0;
            _items[type] += Mathf.Max(0, amount);
        }

        public bool TryConsume(ItemType type, int amount)
        {
            if (GetCount(type) < amount) return false;
            _items[type] -= amount;
            return true;
        }
    }
}
