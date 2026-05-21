using UnityEngine;

namespace Ashfall.Core
{
    /// <summary>
    /// Entry point that wires managers for additive scene workflow.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private Ashfall.Save.SaveSystem saveSystem;
        [SerializeField] private Ashfall.Audio.DynamicMusicController musicController;

        private void Awake()
        {
            Application.targetFrameRate = 120; // 4K-capable headroom target.
            QualitySettings.vSyncCount = 0;

            saveSystem.Initialize();
            phaseController.Initialize();
            musicController.Initialize(phaseController);
        }
    }
}
