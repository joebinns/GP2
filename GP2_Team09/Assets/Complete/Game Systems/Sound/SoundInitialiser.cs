using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Sounds
{
    [AddComponentMenu("Managers/Sound Initialiser")]
    public class SoundInitialiser : MonoBehaviour
    {
        [SF] private AudioSource _ambience = null;
        [SF] private SoundManager _manager = null;

        private static SoundInitialiser s_active = null;


        private void Awake(){
            if (s_active == null){
                _manager.Initialise(_ambience);
                s_active = this;
            
            } else Destroy(this);
        }

		private void OnDestroy(){
			if (s_active != this) return;
            _manager.OnDestroy();
        }
    }
}