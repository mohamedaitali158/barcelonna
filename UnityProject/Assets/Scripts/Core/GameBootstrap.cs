using UnityEngine;

namespace Ashfall.Core
{
    /// <summary>
    /// Example bootstrap wiring.
    /// Integration steps:
    /// 1) Add AuthenticationManager + CloudSaveManager + IntegrityValidationSystem to GameSystems object.
    /// 2) Link references below in Inspector.
    /// 3) Ensure Unity Services packages are installed (see Docs/SecureArchitectureSetup.md).
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GamePhaseController phaseController;
        [SerializeField] private Ashfall.Save.SaveSystem saveSystem;
        [SerializeField] private Ashfall.Audio.DynamicMusicController musicController;
        [SerializeField] private Ashfall.Audio.CinematicTransitionAudio transitionAudio;
        [SerializeField] private Ashfall.UI.CinematicTransitionController transitionController;
        [SerializeField] private Ashfall.Authentication.AuthenticationManager authenticationManager;
        [SerializeField] private Ashfall.Cloud.CloudSaveManager cloudSaveManager;

        private async void Awake()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;

            saveSystem.Initialize();
            phaseController.Initialize();
            musicController.Initialize(phaseController);
            transitionAudio.Bind(phaseController);
            transitionController.Bind(phaseController);

            if (authenticationManager)
            {
                await authenticationManager.InitializeAndAutoLoginAsync();
                if (cloudSaveManager)
                    await cloudSaveManager.MergeCloudToLocalAsync();
            }
        }
    }
}
