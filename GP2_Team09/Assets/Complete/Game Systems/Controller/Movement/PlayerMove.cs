using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    public class PlayerMove : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;

        private bool _running = false;
        private Vector3 _direction = Vector3.zero;
        
// INITIALISATION

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeVec3(OnMoveInput, InputType.Move, Priority);
            _input.SubscribeKey(OnRunInput, InputType.Run, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeVec3(OnMoveInput, InputType.Move);
            _input.UnsubscribeKey(OnRunInput, InputType.Run);
        }
        
// INPUT

        /// <summary>
        /// Updates move direction on input callback
        /// </summary>
        private void OnMoveInput(Vector3 direction){
            _direction = direction;
        }

        /// <summary>
        /// Toggles the run speed on input callback
        /// </summary>
        private void OnRunInput(){
            _running = !_running;
        }

// MOVEMENT

        /// <summary>
        /// Checks move direction on controller update
        /// </summary>
        public override bool OnCheck(){
            return _direction != Vector3.zero;
        }

        /// <summary>
        /// Moves character controller on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime){
            var direction = transform.TransformDirection(_direction);
            var speed = _running ? _settings.RunSpeed : _settings.MoveSpeed;

            _characterController.Move(direction * speed * deltaTime);
        }
    }
}
