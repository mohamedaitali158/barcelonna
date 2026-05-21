using UnityEngine;

namespace Ashfall.Data
{
    [CreateAssetMenu(menuName = "Ashfall/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public ItemType type;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public int maxStack = 10;
    }
}
