using GameProject.Interactions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameProject
{
    public class Prompt : MonoBehaviour
    {
        [SerializeField] private Image _desktopImage;
        [SerializeField] private Image _consoleImage;
        [SerializeField] private TextMeshProUGUI _verbTMP;

        /// <summary>
        /// Update the displayed prompt sprites and text to the parameter passed
        /// </summary>
        public void SetPrompt(PromptInfo prompt) {
            TogglePrompt(true);
            _desktopImage.sprite = prompt.DesktopIcon;
            _consoleImage.sprite = prompt.ConsoleIcon;
            _verbTMP.text = prompt.Verb;
        }

        /// <summary>
        /// Toggle the prompts visibility
        /// </summary>
        public void TogglePrompt(bool enabled) {
            gameObject.SetActive(enabled);
        }
    }
}
