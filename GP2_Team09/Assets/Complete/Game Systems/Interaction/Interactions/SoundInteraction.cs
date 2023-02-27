using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions {
    public class SoundInteraction : BaseInteraction 
    {
#if UNITY_EDITOR
        [SF, TextArea]
        private string _description = string.Empty;
        [Space]
#endif

        [SF] private bool _playOnce = false;
        [SF] private bool _usePitch = false;
        [SF] private Vector2 _pitchShift = new Vector2(0.95f, 1.05f);
        [SF] private AudioSource _sound = null;

        public override void Enable() => this.enabled = true;
        public override void Disable() => this.enabled = false;

        public override void Changed(float value) => Play();
        public override void Changed(BaseInteraction sender, float value) => Play();

        public override void Play(){
            if (!this.enabled) return;

            if (_usePitch){
                _sound.pitch = Random.Range(
                    _pitchShift.x, _pitchShift.y
                );
            }
            
            if (_playOnce){ 
                this.enabled = false;
            }

            _sound.Play();
        }
        
        public override void Stop(){
            if (!this.enabled) return;
            _sound.Stop();
        }

        public override void Pause(){
            if (!this.enabled) return;
            _sound.Pause();
        }
    }
}