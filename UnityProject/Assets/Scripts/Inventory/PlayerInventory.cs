using System;
using System.Collections.Generic;
using Ashfall.Data;
using UnityEngine;

namespace Ashfall.Inventory
{
    /// <summary>
    /// Lightweight player inventory for scavenging phase.
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        private readonly Dictionary<ItemType, int> _items = new();

        public event Action<ItemType, int> OnItemChanged;

        public int Get(ItemType type) => _items.TryGetValue(type, out var count) ? count : 0;

        public void Add(ItemType type, int amount = 1)
        {
            if (amount <= 0) return;
            _items[type] = Get(type) + amount;
            OnItemChanged?.Invoke(type, _items[type]);
        }

        public bool Spend(ItemType type, int amount = 1)
        {
            if (amount <= 0 || Get(type) < amount) return false;
            _items[type] -= amount;
            OnItemChanged?.Invoke(type, _items[type]);
            return true;
        }
    }
}
