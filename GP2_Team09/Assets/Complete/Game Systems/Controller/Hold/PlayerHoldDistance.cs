using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Movement;

namespace GameProject.Hold
{
    public class PlayerHoldDistance : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null; // TODO: Change this to Interaction settings
        [SF] private InputManager _input = null;
        
        private float _direction = 0;
        
// INITIALISATION

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeFloat(OnReachInput, InputType.Reach, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeFloat(OnReachInput, InputType.Reach);
        }
        
// INPUT

        /// <summary>
        /// Updates move direction on input callback
        /// </summary>
        private void OnReachInput(float direction){
            _direction = direction;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks move direction on controller update
        /// </summary>
        public override bool OnCheck(){
            return _direction != 0;
        }

        /// <summary>
        /// Moves character controller on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime){
            Debug.Log(_direction);
            //_characterController.Move(transform.TransformDirection(_direction * _settings.MoveSpeed * deltaTime));
        }
    }
}
