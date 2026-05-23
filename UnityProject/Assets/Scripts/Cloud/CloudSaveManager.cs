using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ashfall.Authentication;
using Ashfall.Profile;
using Ashfall.Save;
using Unity.Services.CloudSave;
using UnityEngine;

namespace Ashfall.Cloud
{
    /// <summary>
    /// Cloud Save sync layer for secure player profile.
    /// Package requirement: com.unity.services.cloudsave
    /// </summary>
    public class CloudSaveManager : MonoBehaviour
    {
        [SerializeField] private AuthenticationManager authenticationManager;
        [SerializeField] private SaveSystem saveSystem;

        private const string CloudKey = "secure_player_profile_v1";

        public async Task PushProfileAsync(SecurePlayerProfile profile)
        {
            EnsureReady();

            profile.playerId = authenticationManager.PlayerId;
            profile.localSimulationSave = saveSystem.Current;
            profile.dayCount = saveSystem.Current.simulation.day;
            profile.StampTime(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            var payload = new Dictionary<string, object>
            {
                { CloudKey, JsonUtility.ToJson(profile) }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(payload);
        }

        public async Task<SecurePlayerProfile> PullProfileAsync()
        {
            EnsureReady();

            var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { CloudKey });
            if (!result.TryGetValue(CloudKey, out var raw)) return null;

            string json = raw.Value.GetAsString();
            return JsonUtility.FromJson<SecurePlayerProfile>(json);
        }

        public async Task MergeCloudToLocalAsync()
        {
            SecurePlayerProfile cloud = await PullProfileAsync();
            if (cloud?.localSimulationSave == null) return;

            // Basic anti-corruption guard: prefer newest profile only.
            if (cloud.updatedAtUnix <= saveSystem.Current.meta.unixTimestamp) return;

            saveSystem.OverrideCurrent(cloud.localSimulationSave);
            saveSystem.SaveSync();
        }

        private void EnsureReady()
        {
            if (!authenticationManager || !authenticationManager.IsSignedIn)
                throw new InvalidOperationException("CloudSaveManager requires signed-in AuthenticationManager.");
            if (!saveSystem)
                throw new InvalidOperationException("CloudSaveManager requires SaveSystem reference.");
        }
    }
}
