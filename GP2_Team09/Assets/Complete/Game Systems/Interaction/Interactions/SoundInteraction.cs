using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions {
    public class SoundInteraction : BaseInteraction 
    {
        [SF] private bool _usePitch = false;
        [SF] private Vector2 _pitchShift = new Vector2(0.95f, 1.05f);
        [SF] private AudioSource _sound = null;

        public override void Play(){
            if (_usePitch){
                _sound.pitch = Random.Range(
                    _pitchShift.x, _pitchShift.y
                );
            }
            _sound.Play();
        }
        public override void Stop()  => _sound.Stop();
        public override void Pause() => _sound.Pause();
    }
}