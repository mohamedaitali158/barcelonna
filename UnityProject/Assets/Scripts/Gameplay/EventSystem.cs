using System.Collections.Generic;
using UnityEngine;

namespace Ashfall.Gameplay
{
    [System.Serializable]
    public class EventEntry
    {
        public string id;
        public string description;
        public float weight = 1f;
        public bool isHiddenRare;
    }

    public class EventSystem : MonoBehaviour
    {
        [SerializeField] private List<EventEntry> dailyEvents = new();

        public EventEntry RollEvent(bool includeHidden)
        {
            var pool = new List<EventEntry>();
            foreach (var evt in dailyEvents)
            {
                if (!evt.isHiddenRare || includeHidden)
                    pool.Add(evt);
            }

            float total = 0f;
            foreach (var evt in pool) total += evt.weight;

            float roll = Random.Range(0f, total);
            float cumulative = 0f;
            foreach (var evt in pool)
            {
                cumulative += evt.weight;
                if (roll <= cumulative) return evt;
            }

            return pool.Count > 0 ? pool[0] : null;
        }
    }
}
