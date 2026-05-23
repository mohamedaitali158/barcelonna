using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ashfall.Input
{
    public class ControlProfileSaveSystem : MonoBehaviour
    {
        private string PathFor(string platformKey) => Path.Combine(Application.persistentDataPath, $"bindings_{platformKey}.json");

        public void SaveBindingOverrides(InputActionAsset asset, string platformKey)
        {
            File.WriteAllText(PathFor(platformKey), asset.SaveBindingOverridesAsJson());
        }

        public void LoadBindingOverrides(InputActionAsset asset, string platformKey)
        {
            string path = PathFor(platformKey);
            if (!File.Exists(path)) return;
            asset.LoadBindingOverridesFromJson(File.ReadAllText(path));
        }
    }
}
