using System;
using System.Collections.Generic;
using Ashfall.Save;

namespace Ashfall.Profile
{
    [Serializable]
    public class IntMapEntry
    {
        public string key;
        public int value;
    }

    /// <summary>
    /// Canonical profile payload for secure local/cloud persistence.
    /// Keep this aligned with SaveSystem v2+ simulation data.
    /// </summary>
    [Serializable]
    public class SecurePlayerProfile
    {
        public string playerId;
        public int schemaVersion = 1;
        public long updatedAtUnix;

        // Core progression
        public int dayCount;
        public SaveData localSimulationSave;

        // Mirrors for integrity checks and cloud sync (JsonUtility-safe).
        public List<IntMapEntry> inventory = new();
        public List<string> hiddenFlags = new();
        public List<IntMapEntry> hiddenCounters = new();

        // Rare ending chain telemetry.
        public string activeRareEndingCandidate;
        public List<string> resolvedRareEndings = new();

        public void StampTime(long unix) => updatedAtUnix = unix;
    }
}
