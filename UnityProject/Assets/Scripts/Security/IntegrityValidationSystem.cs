using System;
using System.Collections.Generic;
using Ashfall.Save;
using UnityEngine;

namespace Ashfall.Security
{
    /// <summary>
    /// Lightweight anti-cheat integrity checks for impossible values and runtime tampering.
    /// Expandable for multiplayer authoritative validation later.
    /// </summary>
    public class IntegrityValidationSystem : MonoBehaviour
    {
        [SerializeField] private int maxAllowedDay = 20000;
        [SerializeField] private int maxInventoryStack = 9999;

        public event Action<string> OnSuspiciousDetected;

        public bool ValidateSaveData(SaveData data)
        {
            if (data == null || data.simulation == null)
                return Flag("SaveData null or malformed.");

            if (data.simulation.day < 0 || data.simulation.day > maxAllowedDay)
                return Flag($"Impossible day value: {data.simulation.day}");

            if (data.simulation.fungusLevel < 0f || data.simulation.fungusLevel > 100f)
                return Flag($"Invalid fungus level: {data.simulation.fungusLevel}");

            foreach (var c in data.simulation.hiddenCounters.ToDictionary())
            {
                if (c.Value < 0 || c.Value > 1000000)
                    return Flag($"Hidden counter out of range: {c.Key}={c.Value}");
            }

            return true;
        }

        public bool ValidateInventorySnapshot(List<Ashfall.Profile.IntMapEntry> entries)
        {
            foreach (var item in entries)
            {
                if (item.value < 0 || item.value > maxInventoryStack)
                    return Flag($"Impossible inventory count for {item.key}: {item.value}");
            }

            return true;
        }

        private bool Flag(string message)
        {
            Debug.LogWarning($"[IntegrityValidation] {message}");
            OnSuspiciousDetected?.Invoke(message);
            return false;
        }
    }
}
