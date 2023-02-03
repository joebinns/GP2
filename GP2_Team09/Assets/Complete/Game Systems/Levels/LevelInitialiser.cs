using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Levels
{
    [AddComponentMenu("Managers/Level Initialiser")]
    public class LevelInitialiser : MonoBehaviour
    {
        [SF] private LevelManager _manager = null;
        private static LevelInitialiser s_active = null;

        private void Awake(){
            if (s_active == null){
                _manager.Initialise();
                s_active = this;

            } else Destroy(this);
        }

        private void OnDestroy(){
            if (s_active != this) return;
            _manager.OnDestroy();
        }
    }
}