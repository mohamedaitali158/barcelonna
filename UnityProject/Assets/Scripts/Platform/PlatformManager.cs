using System;
using UnityEngine;

namespace Ashfall.Platform
{
    public enum PlatformTier { PC, Android, IOS, PlayStation, Xbox, SteamDeck }

    public class PlatformManager : MonoBehaviour
    {
        public PlatformTier Current { get; private set; }
        public event Action<PlatformTier> OnPlatformDetected;

        private void Awake()
        {
            Current = DetectPlatform();
            OnPlatformDetected?.Invoke(Current);
        }

        public PlatformTier DetectPlatform()
        {
            if (Application.platform == RuntimePlatform.Android) return PlatformTier.Android;
            if (Application.platform == RuntimePlatform.IPhonePlayer) return PlatformTier.IOS;
            if (Application.platform == RuntimePlatform.PS4 || Application.platform == RuntimePlatform.PS5) return PlatformTier.PlayStation;
            if (Application.platform == RuntimePlatform.XboxOne || Application.platform == RuntimePlatform.GameCoreXboxOne || Application.platform == RuntimePlatform.GameCoreXboxSeries) return PlatformTier.Xbox;

            // Steam Deck usually runs Linux with gamepad-first UX.
            if (Application.platform == RuntimePlatform.LinuxPlayer && SystemInfo.deviceModel.Contains("Steam", StringComparison.OrdinalIgnoreCase))
                return PlatformTier.SteamDeck;

            return PlatformTier.PC;
        }
    }
}
