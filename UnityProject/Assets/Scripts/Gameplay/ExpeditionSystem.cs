using Ashfall.Data;
using Ashfall.Systems;
using UnityEngine;

namespace Ashfall.Gameplay
{
    public class ExpeditionSystem : MonoBehaviour
    {
        [SerializeField] private InventorySystem inventory;

        public bool LaunchExpedition(bool hasMap, bool hasGasMask, int ammoToTake)
        {
            if (hasMap && inventory.GetCount(ItemType.Map) <= 0) return false;
            if (hasGasMask && inventory.GetCount(ItemType.GasMask) <= 0) return false;
            if (ammoToTake > 0 && !inventory.TryConsume(ItemType.Ammo, ammoToTake)) return false;
            return true;
        }
    }
}
