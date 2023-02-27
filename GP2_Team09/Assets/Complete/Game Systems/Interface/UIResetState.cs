using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interface
{
    public class UIResetState : MonoBehaviour
    {
        [SF] private GameObject _startScreen = null;
        [Space]
        [SF] private bool _exclude = false;
        [SF] private GameObject _spaceship = null;
        [SF] private GameObject _satelite  = null;

        /// <summary>
        /// Resets the interface, only showing the start screen
        /// </summary>
        public void ResetInterface(){
            if (_startScreen == null) return;

            int ship = _exclude ? _spaceship.transform.GetSiblingIndex() : 0;
            int sat = _exclude  ? _satelite.transform.GetSiblingIndex()  : 0;

            for (int i = 0; i < transform.childCount; i++){
                if (_exclude && (i == ship || i == sat)) continue;
                transform.GetChild(i).gameObject.SetActive(false);
            }

            _startScreen.SetActive(true);
        }
    }
}