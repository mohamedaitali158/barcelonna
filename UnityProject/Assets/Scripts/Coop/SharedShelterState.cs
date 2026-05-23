using System;
using Ashfall.Save;

namespace Ashfall.Coop
{
    [Serializable]
    public class SharedShelterState
    {
        public SaveData sharedSaveData;
        public int sharedInventoryRevision;
        public int sharedEventRevision;
        public bool p1Down;
        public bool p2Down;
        public int hostTick;
    }
}
