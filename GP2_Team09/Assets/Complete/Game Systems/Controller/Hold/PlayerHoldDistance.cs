using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Interactions;
using GameProject.Movement;
using UnityEngine;

namespace GameProject.Hold
{
    public class PlayerHoldDistance : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private Transform _holdPivot;
        [Space]
        [SF] private InteractionSettings _settings = null; // TODO: Change this to Interaction settings
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
        /// Updates hold distance direction on input callback
        /// </summary>
        private void OnReachInput(float direction){
            _direction = direction;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks hold distance direction on controller update
        /// </summary>
        public override bool OnCheck(){
            return _direction != 0; // TODO: Additionally check if holding something
        }

        /// <summary>
        /// Moves hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
            var localPosition = _holdPivot.localPosition;
            var holdDistanceRange = _settings.HoldDistanceRange;
            var reach = Mathf.Clamp(localPosition.z + _direction * _settings.HoldDistanceSensitivity * deltaTime, holdDistanceRange.x, holdDistanceRange.y);
            localPosition.z = reach;
            _holdPivot.localPosition = localPosition;
        }
    }
}
