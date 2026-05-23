using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ashfall.Input
{
    /// <summary>
    /// Runtime rebinding with per-platform persistence hooks.
    /// Setup: assign InputActionAsset with all gameplay/UI maps.
    /// </summary>
    public class InputRebindingManager : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private ControlProfileSaveSystem profileSave;

        public event Action OnBindingsChanged;

        private string PlatformKey => Application.platform.ToString();

        private void Awake()
        {
            if (profileSave && actions)
                profileSave.LoadBindingOverrides(actions, PlatformKey);
        }

        public async Task StartRebindAsync(string actionName, int bindingIndex)
        {
            var action = actions.FindAction(actionName, true);
            if (action == null) return;

            action.Disable();
            var op = action.PerformInteractiveRebinding(bindingIndex)
                .WithCancelingThrough("<Keyboard>/escape");

            var tcs = new TaskCompletionSource<bool>();
            op.OnComplete(_ => tcs.TrySetResult(true));
            op.OnCancel(_ => tcs.TrySetResult(false));
            op.Start();

            await tcs.Task;
            op.Dispose();
            action.Enable();

            profileSave?.SaveBindingOverrides(actions, PlatformKey);
            OnBindingsChanged?.Invoke();
        }

        public void RestoreDefaults()
        {
            actions.RemoveAllBindingOverrides();
            profileSave?.SaveBindingOverrides(actions, PlatformKey);
            OnBindingsChanged?.Invoke();
        }
    }
}
