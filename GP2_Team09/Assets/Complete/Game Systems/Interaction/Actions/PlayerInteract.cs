using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameProject.Actions;
using GameProject.Movement;
using GameProject.Inputs;
using GameProject.Cameras;
using GameProject.HUD;

namespace GameProject.Interactions
{
    public class PlayerInteract : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private HUDManager _hudManager = null;
        [Space]
        [SF] private InteractionSettings _settings = null;
        [SF] private InputManager _input = null;
        [SF] private CameraManager _camera = null;

        private bool _pressed = false;
        private List<IInteractable> _actions = new();
        private List<IInteractable> _triggered = new();
        private IInteractable _outlined = null;

// INITIALISATION

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.LateUpdate);
            _input.SubscribeKey(OnUseInput, InputType.Use, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.UnsubscribeKey(OnUseInput, InputType.Use);
        }

// OUTLINE

        /// <summary>
        /// Check the available actions
        /// </summary>
        public void Update() {
            if (_triggered.Count > 0) return;

            var actions = GetActions();
            UpdateInteractable(actions);

            _actions = actions;
        }

        /// <summary>
        /// Updates the interaction outline
        /// </summary>
        private void UpdateInteractable(List<IInteractable> actions) {
            // Get current and previous action
            IInteractable action = null;
            if (actions != null && actions.Count > 0) {
                action = actions[0];
            }
            IInteractable prevAction = null;
            if (_actions != null && _actions.Count > 0) {
                prevAction = _actions[0];
            }
            
            // When action changes...
            if (action != prevAction) {
                // Remove outline of previous action
                if (prevAction != null) {
                    Outline(null);
                }
                
                // If a new action has been hovered, switch HUD to hover mode and outline the new action
                if (action != null) {
                    _hudManager.SwitchInteractable(action.InteractableType, InteractableMode.Hover);
                    Outline(action);
                }
                // If there is no new action hovered, switch HUD to default mode and remove any outline
                else {
                    _hudManager.SwitchInteractable(InteractableType.None, InteractableMode.None);
                    Outline(null);
                }
            }
        }

        /// <summary>
        /// Enable outline on a relevant interactable or disable on a previously relevant interactable
        /// </summary>
        private void Outline(IInteractable interactable) {
            if (interactable != null){
                if (interactable.Outline == null) return;
                // Enable outline
                interactable.Outline.enabled = true;
                _outlined = interactable;
            }
            else { // Disable outline
                if (_outlined == null) return;
                if (_outlined.Outline == null) return;
                _outlined.Outline.enabled = false;
                _outlined = null;
            }
        }

// INTERACTION    

        /// <summary>
        /// Triggers interactable on input callback
        /// </summary>
        private void OnUseInput(){
            _pressed = _triggered.Count == 0;

            if (_triggered.Count > 0){
                if (_triggered != null && _triggered.Count > 0){
                    _hudManager.SwitchInteractable(
                        _triggered[0].InteractableType, 
                        InteractableMode.Hover
                    );
                }
                ClearSelected();
            }
            
            if (_pressed){
                GetSelected();

                if (_triggered != null && _triggered.Count > 0){ 
                    _hudManager.SwitchInteractable(
                        _triggered[0].InteractableType, 
                        InteractableMode.Action
                    );
                }
            }
        }

        /// <summary>
        /// Stores interactables
        /// </summary>
        private void GetSelected(){
            for (int i = 0; i < _actions.Count; i++){
                var action = _actions[i];

                action?.Perform(_pressed);
                _triggered.Add(action);
            }
        }

        /// <summary>
        /// Releases the stored interactables
        /// </summary>
        private void ClearSelected(){
            for (int i = 0; i < _triggered.Count; i++)
                _triggered[i]?.Perform(false);

            _triggered.Clear();
        }

        /// <summary>
        /// Get all intercepted actions
        /// </summary>
        private List<IInteractable> GetActions() {
            var camera   = _camera.Transform;
            var radius   = _settings.Radius;
            var distance = _settings.Distance;
            var mask     = _settings.Mask;

            var ray = new Ray(camera.position, camera.forward);
            var actions = new List<IInteractable>();
            
            if (Physics.SphereCast(ray, radius, out var hit, distance, mask)){
                actions = hit.collider.GetComponents<IInteractable>().ToList();
                for (var i = actions.Count-1; i >= 0; i--) {
                    if (!actions[i].IsActive)
                        actions.RemoveAt(i);
                }
            }

            return actions;
        }

// INTERFACE

        /// <summary>
        /// Switches the interaction reticle
        /// </summary>
        private void SwitchInteractable() {
            if (_triggered == null || 
                _triggered.Count == 0) 
                return;

            _hudManager.SwitchInteractable(
                _triggered[0].InteractableType, 
                InteractableMode.Action
            );
        }
    }
}