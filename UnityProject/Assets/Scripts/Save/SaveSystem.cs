using System;
using System.IO;
using UnityEngine;

namespace Ashfall.Save
{
    [Serializable]
    public class SaveData
    {
        public int day;
        public float householdStress;
        public float fungusLevel;
        public string endingKey;
    }

    public class SaveSystem : MonoBehaviour
    {
        private string SavePath => Path.Combine(Application.persistentDataPath, "ashfall_save.json");
        public SaveData Current { get; private set; } = new();

        public void Initialize()
        {
            if (File.Exists(SavePath))
                Load();
        }

        public void Save()
        {
            var json = JsonUtility.ToJson(Current, true);
            File.WriteAllText(SavePath, json);
        }

        public void Load()
        {
            var json = File.ReadAllText(SavePath);
            Current = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
