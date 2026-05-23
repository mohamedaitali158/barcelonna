using UnityEngine;

namespace Ashfall.AI
{
    /// <summary>
    /// Basic utility-driven behavior placeholder for advanced AI state trees.
    /// </summary>
    public class SurvivorAIController : MonoBehaviour
    {
        public enum SurvivorState { Idle, Patrol, Scavenge, Panic, Combat }

        [SerializeField] private SurvivorState currentState;
        [SerializeField] private float dangerAwareness;

        private void Update()
        {
            if (dangerAwareness > 80f) currentState = SurvivorState.Panic;
            else if (dangerAwareness > 55f) currentState = SurvivorState.Combat;
            else currentState = SurvivorState.Scavenge;
        }
    }
}
