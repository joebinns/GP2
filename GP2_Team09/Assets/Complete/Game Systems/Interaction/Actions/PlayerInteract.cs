using SF = UnityEngine.SerializeField;
using System;
using UnityEngine;
using GameProject.Actions;
using GameProject.Movement;
using GameProject.Inputs;
using GameProject.Cameras;
using System.Collections.Generic;
using System.Linq;

namespace GameProject.Interactions
{
    public class PlayerInteract : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [Space]
        [SF] private InteractionSettings _settings = null;
        [SF] private InputManager _input = null;
        [SF] private CameraManager _camera = null;

        private List<IInteractable> _actions = new();
        private List<IInteractable> _triggered = new();
        

// INITIALISATION

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.LateUpdate);
            _input.SubscribeKey(OnActionInput, InputType.Use, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.UnsubscribeKey(OnActionInput, InputType.Use);
        }

// INTERACTION

        /// <summary>
        /// Triggers interactable on input callback
        /// </summary>
        private void OnActionInput(){
            if (_triggered.Count > 0)
                 ClearSelected();
            else GetSelected();
        }

        /// <summary>
        /// Stores interactables
        /// </summary>
        private void GetSelected() {
            foreach (var action in _actions) {
                action.Trigger();
                _triggered.Add(action);
            }
        }
        
        /// <summary>
        /// Check the available actions
        /// </summary>
        public void Update() {
            var actions = GetActions();
            if (actions != _actions) OnActionsChanged?.Invoke(actions); // TODO: This is being called even when the lists are the same...
            _actions = actions;
        }

        /// <summary>
        /// Get all intercepted actions
        /// </summary>
        private List<IInteractable> GetActions() {
            var camera = _camera.Transform;
            var radius = _settings.Radius;
            var distance = _settings.Distance;
            var mask = _settings.Mask;

            var ray = new Ray(camera.position, camera.forward);
            
            var actions = new List<IInteractable>();
            if (Physics.SphereCast(ray, radius, out var hit, distance, mask)) {
                actions = hit.collider.GetComponents<IInteractable>().ToList();
            }
            return actions;
        }

        /// <summary>
        /// Releases the stored interactables
        /// </summary>
        private void ClearSelected(){
            for (int i = 0; i < _triggered.Count; i++){
                _triggered[i].Trigger();
            }

            _triggered.Clear();
        }

        public event Action<List<IInteractable>> OnActionsChanged;
    }
}