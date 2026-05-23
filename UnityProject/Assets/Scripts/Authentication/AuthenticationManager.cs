using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Ashfall.Authentication
{
    public enum AuthProvider
    {
        Anonymous,
        Google,
        Steam
    }

    /// <summary>
    /// Unity Authentication bootstrap.
    /// Auto-login on startup (anonymous) and extension points for Google/Steam.
    /// Package requirements:
    /// - com.unity.services.core
    /// - com.unity.services.authentication
    /// </summary>
    public class AuthenticationManager : MonoBehaviour
    {
        public event Action<string> OnLoginSuccess;
        public event Action<string> OnLoginFailed;

        public bool IsSignedIn => AuthenticationService.Instance.IsSignedIn;
        public string PlayerId => IsSignedIn ? AuthenticationService.Instance.PlayerId : string.Empty;

        private async void Awake()
        {
            await InitializeAndAutoLoginAsync();
        }

        public async Task InitializeAndAutoLoginAsync()
        {
            try
            {
                if (UnityServices.State != ServicesInitializationState.Initialized)
                    await UnityServices.InitializeAsync();

                await LoginAsync(AuthProvider.Anonymous);
            }
            catch (Exception ex)
            {
                OnLoginFailed?.Invoke($"Services init/login failed: {ex.Message}");
            }
        }

        public async Task LoginAsync(AuthProvider provider)
        {
            try
            {
                switch (provider)
                {
                    case AuthProvider.Anonymous:
                        if (!AuthenticationService.Instance.IsSignedIn)
                            await AuthenticationService.Instance.SignInAnonymouslyAsync();
                        break;
                    case AuthProvider.Google:
                        throw new NotImplementedException("Google login adapter placeholder. Wire OAuth token flow here.");
                    case AuthProvider.Steam:
                        throw new NotImplementedException("Steam login adapter placeholder. Wire Steam ticket flow here.");
                }

                OnLoginSuccess?.Invoke(AuthenticationService.Instance.PlayerId);
            }
            catch (Exception ex)
            {
                OnLoginFailed?.Invoke($"Login failed ({provider}): {ex.Message}");
            }
        }
    }
}
