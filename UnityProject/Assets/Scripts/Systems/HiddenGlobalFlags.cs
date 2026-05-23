using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ashfall.Systems
{
    /// <summary>
    /// Persistent hidden flags and counters for impossible "Reality Fracture" ending chains.
    /// </summary>
    public class HiddenGlobalFlags : MonoBehaviour
    {
        private readonly HashSet<string> _boolFlags = new();
        private readonly Dictionary<string, int> _counters = new();

        public event Action<string> OnFlagSet;

        public void Set(string flag)
        {
            if (string.IsNullOrWhiteSpace(flag)) return;
            if (_boolFlags.Add(flag)) OnFlagSet?.Invoke(flag);
        }

        public bool Has(string flag) => _boolFlags.Contains(flag);

        public void Increment(string counter, int amount = 1)
        {
            if (string.IsNullOrWhiteSpace(counter) || amount <= 0) return;
            if (!_counters.ContainsKey(counter)) _counters[counter] = 0;
            _counters[counter] += amount;
        }

        public int GetCounter(string counter) => _counters.TryGetValue(counter, out var value) ? value : 0;

        public List<string> SnapshotFlags() => new(_boolFlags);
        public Dictionary<string, int> SnapshotCounters() => new(_counters);

        public void Restore(List<string> flags, Dictionary<string, int> counters)
        {
            _boolFlags.Clear();
            _counters.Clear();

            if (flags != null)
                foreach (var flag in flags)
                    _boolFlags.Add(flag);

            if (counters != null)
                foreach (var pair in counters)
                    _counters[pair.Key] = pair.Value;
        }
    }
}
