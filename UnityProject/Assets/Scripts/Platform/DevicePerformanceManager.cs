using UnityEngine;
using UnityEngine.Rendering;

namespace Ashfall.Platform
{
    public class DevicePerformanceManager : MonoBehaviour
    {
        [SerializeField] private PlatformManager platformManager;
        [SerializeField] private int targetFpsHigh = 60;
        [SerializeField] private int targetFpsMobile = 45;

        private void Start()
        {
            ApplyProfile(platformManager ? platformManager.Current : PlatformTier.PC);
        }

        public void ApplyProfile(PlatformTier tier)
        {
            switch (tier)
            {
                case PlatformTier.Android:
                case PlatformTier.IOS:
                    Application.targetFrameRate = targetFpsMobile;
                    QualitySettings.SetQualityLevel(1, true);
                    ScalableBufferManager.ResizeBuffers(0.8f, 0.8f);
                    break;
                case PlatformTier.SteamDeck:
                    Application.targetFrameRate = 60;
                    QualitySettings.SetQualityLevel(2, true);
                    ScalableBufferManager.ResizeBuffers(0.9f, 0.9f);
                    break;
                case PlatformTier.PlayStation:
                case PlatformTier.Xbox:
                    Application.targetFrameRate = 60;
                    QualitySettings.SetQualityLevel(3, true);
                    ScalableBufferManager.ResizeBuffers(1f, 1f);
                    break;
                default:
                    Application.targetFrameRate = targetFpsHigh;
                    QualitySettings.SetQualityLevel(4, true);
                    ScalableBufferManager.ResizeBuffers(1f, 1f);
                    break;
            }
        }
    }
}
