using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

namespace Ashfall.Accounts
{
    public class AccountLinkSystem : MonoBehaviour
    {
        /// <summary>
        /// Link guest account with Google token to preserve progression continuity.
        /// </summary>
        public async Task LinkGoogleAsync(string googleIdToken)
        {
            if (!AuthenticationService.Instance.IsSignedIn) return;
            await AuthenticationService.Instance.LinkWithGoogleAsync(googleIdToken);
        }
    }
}
