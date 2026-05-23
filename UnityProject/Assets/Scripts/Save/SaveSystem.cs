using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ashfall.Security;
using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Save
{
    [Serializable]
    public class SerializableIntDictionary
    {
        public List<string> keys = new();
        public List<int> values = new();

        public Dictionary<string, int> ToDictionary()
        {
            var dict = new Dictionary<string, int>();
            int n = Mathf.Min(keys.Count, values.Count);
            for (int i = 0; i < n; i++) dict[keys[i]] = values[i];
            return dict;
        }

        public void FromDictionary(Dictionary<string, int> dict)
        {
            keys.Clear();
            values.Clear();
            if (dict == null) return;
            foreach (var pair in dict)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
    }

    [Serializable] public class CharacterState { public string id; public bool isDead; public float relationship; public float stress; public string disease; public float talkativeness = 1f; public float socialTrust = 1f; public float movementSpeedFactor = 1f; public float obedience = 1f; public float isolationNeed; public float nervousLaughter; }
    [Serializable] public class WorldEventState { public string eventId; public bool resolved; }
    [Serializable] public class MetaChunk { public int version = 2; public string slotId = "default"; public long unixTimestamp; }

    [Serializable]
    public class SimulationChunk
    {
        public int day;
        public float householdStress;
        public float fungusLevel;
        public string endingKey;
        public List<CharacterState> characters = new();
        public List<string> hiddenFlags = new();
        public SerializableIntDictionary hiddenCounters = new();
        public List<string> rebellions = new();
        public List<WorldEventState> worldEvents = new();
    }

    [Serializable] public class SaveData { public MetaChunk meta = new(); public SimulationChunk simulation = new(); }

    /// <summary>
    /// SaveSystem v2+ with encrypted local persistence, checksum validation, and auto backups.
    /// </summary>
    public class SaveSystem : MonoBehaviour
    {
        [SerializeField] private string slotId = "default";
        [SerializeField] private HiddenGlobalFlags hiddenGlobalFlags;
        [SerializeField] private IntegrityValidationSystem integrityValidator;

        private string SavePath => Path.Combine(Application.persistentDataPath, $"ashfall_save_v2_{slotId}.dat");
        private string BackupPath => Path.Combine(Application.persistentDataPath, $"ashfall_save_v2_{slotId}.bak.dat");
        public SaveData Current { get; private set; } = new();

        public void Initialize()
        {
            Current.meta.slotId = slotId;
            if (File.Exists(SavePath)) Load();
        }

        public void SaveSync()
        {
            SyncFromRuntimeFlags();
            StampMeta();

            string json = JsonUtility.ToJson(Current, true);
            string encrypted = SaveEncryptionUtility.EncryptWithChecksum(json);

            if (File.Exists(SavePath)) File.Copy(SavePath, BackupPath, true);
            File.WriteAllText(SavePath, encrypted);
        }

        public void SaveAsync(MonoBehaviour host)
        {
            if (!host) return;
            host.StartCoroutine(SaveRoutine());
        }

        public void Load()
        {
            try
            {
                string encrypted = File.ReadAllText(SavePath);
                string json = SaveEncryptionUtility.DecryptAndValidate(encrypted);
                Current = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();

                if (!ValidateOrRecover()) return;
                RestoreRuntimeFlags();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Primary save failed to load: {ex.Message}. Trying backup.");
                RecoverFromBackup();
            }
        }

        public void OverrideCurrent(SaveData overrideData)
        {
            Current = overrideData ?? new SaveData();
            if (!ValidateOrRecover()) Current = new SaveData();
            RestoreRuntimeFlags();
        }

        private IEnumerator SaveRoutine()
        {
            SyncFromRuntimeFlags();
            StampMeta();
            string json = JsonUtility.ToJson(Current, true);
            string encrypted = SaveEncryptionUtility.EncryptWithChecksum(json);
            yield return null;

            if (File.Exists(SavePath)) File.Copy(SavePath, BackupPath, true);
            File.WriteAllText(SavePath, encrypted);
        }

        private void RecoverFromBackup()
        {
            if (!File.Exists(BackupPath))
            {
                Current = new SaveData();
                return;
            }

            string encrypted = File.ReadAllText(BackupPath);
            string json = SaveEncryptionUtility.DecryptAndValidate(encrypted);
            Current = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
            ValidateOrRecover();
            RestoreRuntimeFlags();
        }

        private bool ValidateOrRecover()
        {
            if (Current.meta.version < 2) Current.meta.version = 2;
            return !integrityValidator || integrityValidator.ValidateSaveData(Current);
        }

        private void SyncFromRuntimeFlags()
        {
            if (!hiddenGlobalFlags) return;
            Current.simulation.hiddenFlags = hiddenGlobalFlags.SnapshotFlags();
            Current.simulation.hiddenCounters.FromDictionary(hiddenGlobalFlags.SnapshotCounters());
        }

        private void RestoreRuntimeFlags()
        {
            if (!hiddenGlobalFlags) return;
            hiddenGlobalFlags.Restore(Current.simulation.hiddenFlags, Current.simulation.hiddenCounters.ToDictionary());
        }

        private void StampMeta()
        {
            Current.meta.version = 2;
            Current.meta.slotId = slotId;
            Current.meta.unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
