using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    public class PlayerTurn : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;

        private float _direction = 0;
        private Transform _transform = null;

// INITIALISATION

        /// <summary>
        /// Initialises the turn action
        /// </summary>
        private void Awake(){
            _transform = transform;
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeFloat(OnTurnInput, InputType.Turn, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.SubscribeFloat(OnTurnInput, InputType.Turn);
        }

// INPUT

        /// <summary>
        /// Updates turn direction on input callback
        /// </summary>
        private void OnTurnInput(float direction){
            _direction = direction;
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
            var euler = _transform.eulerAngles;

            euler.y += _direction * _settings.TurnSensi;

            _transform.eulerAngles = euler;
        }
    }
}