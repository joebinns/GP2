using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;
using TMPro;
using Unity.VectorGraphics;

namespace GameProject
{
    public class Prompt : MonoBehaviour
    {
        [SF] private SVGImage DesktopImage;
        [SF] private SVGImage ConsoleImage;
        [SF] private TextMeshProUGUI VerbTMP;

        public void SetPrompt(PromptInfo prompt) {
            TogglePrompt(true);
            DesktopImage.sprite = prompt.DesktopIcon;
            ConsoleImage.sprite = prompt.ConsoleIcon;
            VerbTMP.text = prompt.Verb;
        }

        public void TogglePrompt(bool enabled) {
            gameObject.SetActive(enabled);
        }
    }
}
