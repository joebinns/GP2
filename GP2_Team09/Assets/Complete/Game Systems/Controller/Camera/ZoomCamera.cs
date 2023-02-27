using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Movement;
using GameProject.Cameras;
using GameProject.Inputs;
using UnityEngine.Rendering;

namespace GameProject.Actions
{
    public class ZoomCamera : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private CameraSettings _settings = null;
        [SF] private CameraManager _camera    = null;
        [SF] private InputManager _input      = null;

        private bool _zooming  = false;
        private bool _zoomIn   = false;
        private bool _zoomOut  = false;
        private float _current = 0;

        private float _time        = 0;
        private float _inDuration  = 0;
        private float _outDuration = 0;

// INITIALISATION

        /// <summary>
        /// Initialises the field of view
        /// </summary>
        private void Awake(){
            _camera.SetFOV(_settings.DefaultFOV);

            _inDuration = _settings.ZoomInCurve[
                _settings.ZoomInCurve.length - 1
            ].time;

            _outDuration = _settings.ZoomOutCurve[
                _settings.ZoomOutCurve.length - 1
            ].time;
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.Update);

            _input.SubscribeFloat(OnZoomInput, InputType.Zoom, Priority);
            _input.SubscribeKey(OnZoomToggleInput, InputType.Primary, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);

            _input.UnsubscribeFloat(OnZoomInput, InputType.Zoom);
            _input.UnsubscribeKey(OnZoomToggleInput, InputType.Primary);
        }

// INPUT HANDLING

        /// <summary>
        /// Updates zoom state on input callback
        /// </summary>
        private void OnZoomToggleInput(){
            if (_time > 0) return;

            if (_zooming)
                 SetZoomOut();
            else SetZoomIn(); 
        }

        /// <summary>
        /// Updates zoom state on input callback
        /// </summary>
        private void OnZoomInput(float direction){
            if (_time > 0) return;

            if (direction > 0 && !_zoomIn)
                SetZoomIn();

            else if (direction < 0 && !_zoomOut)
                SetZoomOut();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetZoomIn(){
            _current = _camera.FieldOfView;
            _zoomOut = false;
            _zoomIn  = true;
            _zooming = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetZoomOut(){
            _current = _camera.FieldOfView;
            _zooming = false;
            _zoomIn  = false;
            _zoomOut = true;
        }

// ZOOM HANDLING

        /// <summary>
        /// Checks zoom input on controller update
        /// </summary>
        public override bool OnCheck(){
            return (_zoomIn  && _time < _inDuration) ||
                   (_zoomOut && _time < _outDuration);
        }

        /// <summary>
        /// Reset zoom state on controller update complete
        /// </summary>
        public override void OnExit(){
            _zoomIn  = false;
            _zoomOut = false;
            _time    = 0;
        }

        /// <summary>
        /// Changes the camera's field of view on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime){
            if (_zoomIn)
                ZoomIn(_time += deltaTime);

            else if (_zoomOut)
                ZoomOut(_time += deltaTime);
        }


        /// <summary>
        /// Changes the camera's field of view over time
        /// </summary>
        private void ZoomIn(float time){
            var fov = Mathf.Lerp(
                _current,
                _settings.MaxZoomFOV, 
                _settings.ZoomInCurve.Evaluate(time)
            );

            _camera.SetFOV(fov);
        }

        /// <summary>
        /// Changes the camera's field of view over time
        /// </summary>
        private void ZoomOut(float time){
            var fov = Mathf.Lerp(
                _current,
                _settings.DefaultFOV,
                _settings.ZoomOutCurve.Evaluate(time)
            );

            _camera.SetFOV(fov);
        }
    }
}