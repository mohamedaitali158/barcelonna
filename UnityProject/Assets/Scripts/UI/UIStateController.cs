using UnityEngine;

namespace Ashfall.UI
{
    public class UIStateController : MonoBehaviour
    {
        [SerializeField] private Animator menuAnimator;
        [SerializeField] private GameObject tutorialPanel;

        public void OpenTutorial(bool open)
        {
            tutorialPanel.SetActive(open);
            menuAnimator.SetTrigger(open ? "OpenTutorial" : "CloseTutorial");
        }
    }
}
