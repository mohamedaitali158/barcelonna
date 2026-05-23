using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ashfall.Authentication;
using Ashfall.Cloud;
using Unity.Services.Authentication;
using UnityEngine;

namespace Ashfall.Accounts
{
    public class AccountManager : MonoBehaviour
    {
        [SerializeField] private AuthenticationManager auth;
        [SerializeField] private CloudSaveManager cloud;

        public event Action<string> OnAccountSwitched;
        public event Action<List<string>> OnProfilesUpdated;

        private readonly List<string> _localProfiles = new();

        public IReadOnlyList<string> Profiles => _localProfiles;

        public async Task SwitchToGuestAsync()
        {
            await SafeSignOutAsync();
            await auth.LoginAsync(AuthProvider.Anonymous);
            RegisterProfile(auth.PlayerId);
            await cloud.MergeCloudToLocalAsync();
            OnAccountSwitched?.Invoke(auth.PlayerId);
        }

        public async Task SwitchAccountWithTokenAsync(string token, bool isGoogle)
        {
            await SafeSignOutAsync();
            if (isGoogle) await AuthenticationService.Instance.SignInWithGoogleAsync(token);
            else await AuthenticationService.Instance.SignInAnonymouslyAsync();

            RegisterProfile(AuthenticationService.Instance.PlayerId);
            await cloud.MergeCloudToLocalAsync();
            OnAccountSwitched?.Invoke(AuthenticationService.Instance.PlayerId);
        }

        public async Task SafeSignOutAsync()
        {
            if (AuthenticationService.Instance.IsSignedIn)
            {
                await Task.Run(() => AuthenticationService.Instance.SignOut());
            }
        }

        private void RegisterProfile(string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            if (!_localProfiles.Contains(id)) _localProfiles.Add(id);
            OnProfilesUpdated?.Invoke(_localProfiles);
        }
    }
}
