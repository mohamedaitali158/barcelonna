using System;
using UnityEngine;

namespace Ashfall.Coop
{
    public class CoopPlayerSynchronizer : MonoBehaviour
    {
        public event Action<int, Vector3> OnPlayerPositionSync;

        public void SyncPlayer(int playerIndex, Vector3 worldPos)
        {
            OnPlayerPositionSync?.Invoke(playerIndex, worldPos);
        }

        public bool ValidateMoveDelta(Vector3 from, Vector3 to, float maxMetersPerSecond, float dt)
        {
            float speed = Vector3.Distance(from, to) / Mathf.Max(0.001f, dt);
            return speed <= maxMetersPerSecond;
        }
    }
}
