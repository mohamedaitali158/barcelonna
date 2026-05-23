using System.IO;
using Ashfall.Core;
using Ashfall.Save;
using UnityEngine;

namespace Ashfall.Resume
{
    public class ResumeGameSystem : MonoBehaviour
    {
        [SerializeField] private SaveSystem saveSystem;
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private CheckpointManager checkpoints;

        private string ResumePath => Path.Combine(Application.persistentDataPath, "ashfall_resume_snapshot.json");

        public void AutoSaveCriticalTransition(string expeditionState, string eventId)
        {
            var snapshot = checkpoints.BuildSnapshot(expeditionState, eventId);
            File.WriteAllText(ResumePath, JsonUtility.ToJson(snapshot, true));
            saveSystem.SaveSync();
        }

        public bool TryResume()
        {
            if (!File.Exists(ResumePath)) return false;
            var snap = JsonUtility.FromJson<RuntimeStateSnapshot>(File.ReadAllText(ResumePath));
            if (snap == null || snap.saveData == null) return false;

            saveSystem.OverrideCurrent(snap.saveData);
            phaseController.SetPhase(snap.phase);
            return true;
        }
    }
}
