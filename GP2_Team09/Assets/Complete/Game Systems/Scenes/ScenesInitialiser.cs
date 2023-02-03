using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Scenes
{
    [AddComponentMenu("Managers/Scenes Initialiser")]
    public class ScenesInitialiser : MonoBehaviour
	{
		[SF] private ScenesManager _manager = null;
        private static ScenesInitialiser s_active = null;

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