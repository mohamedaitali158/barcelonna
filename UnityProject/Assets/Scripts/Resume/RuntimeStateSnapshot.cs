using System;
using Ashfall.Core;
using Ashfall.Save;

namespace Ashfall.Resume
{
    [Serializable]
    public class RuntimeStateSnapshot
    {
        public SaveData saveData;
        public GamePhase phase;
        public float phaseTimeRemaining;
        public string expeditionState;
        public string activeEventId;
        public float shelterDecay;
        public float behaviorStress;
        public long timestamp;
    }
}
