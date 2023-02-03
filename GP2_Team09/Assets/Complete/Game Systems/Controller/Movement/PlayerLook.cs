using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Cameras;

namespace GameProject.Movement
{
    public class PlayerLook : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private Transform _cameraTarget = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;

        private float _angle = 0;
        private float _direction = 0;

// INITIALISATION

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.LateUpdate);
            _input.SubscribeFloat(OnLookInput, InputType.Tilt, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.SubscribeFloat(OnLookInput, InputType.Tilt);
        }

// INPUT

        /// <summary>
        /// Updates look direction on input callback
        /// </summary>
        private void OnLookInput(float direction){
            _direction = -direction;
        }

// MOVEMENT

        /// <summary>
        /// Checks look input on controller update
        /// </summary>
        public override bool OnCheck(){
            return _direction != 0;
        }

        /// <summary>
        /// Updates camera target rotation on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime){
            _angle += _direction * _settings.LookSpeed * deltaTime;

            if (_angle > _settings.MinMaxLookAngle.y)
                _angle = _settings.MinMaxLookAngle.y;

            else if (_angle < _settings.MinMaxLookAngle.x)
                _angle = _settings.MinMaxLookAngle.x;

            _cameraTarget.localRotation = Quaternion.Euler(_angle, 0, 0);
        }
    }
}