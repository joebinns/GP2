using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.Audio;

namespace GameProject.Sounds
{
    [CreateAssetMenu(menuName = "Managers/Sounds", fileName = "Sound Manager")]
    public class SoundManager : ScriptableObject
    {
        [SF] private AudioMixer _mixer = null;

        private AudioSource _ambience = null;

// INITIALISATION

        /// <summary>
        /// Initialises the sound manager
        /// </summary>
        public void Initialise(AudioSource ambience){
            _ambience = ambience;
        }

        /// <summary>
        /// Deinitialises the sound manager
        /// </summary>
        public void OnDestroy(){}

// AUDIO MANAGEMENT


    }
}