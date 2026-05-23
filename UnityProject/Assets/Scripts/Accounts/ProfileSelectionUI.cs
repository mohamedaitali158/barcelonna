using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ashfall.Accounts
{
    public class ProfileSelectionUI : MonoBehaviour
    {
        [SerializeField] private AccountManager accountManager;
        [SerializeField] private TextMeshProUGUI profileListLabel;

        private void OnEnable()
        {
            if (accountManager)
                accountManager.OnProfilesUpdated += RenderProfiles;
        }

        private void OnDisable()
        {
            if (accountManager)
                accountManager.OnProfilesUpdated -= RenderProfiles;
        }

        private void RenderProfiles(List<string> ids)
        {
            if (!profileListLabel) return;
            profileListLabel.text = string.Join("\n", ids);
        }
    }
}
