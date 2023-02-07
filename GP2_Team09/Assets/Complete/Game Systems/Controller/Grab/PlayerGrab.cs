using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;
using System;

namespace GameProject.Grab
{
    public class PlayerGrab : MonoBehaviour
    {
        [SF] private InteractionSettings _settings = null;
        [Space]
        [SF] private Transform _cameraTarget;
        [SF] private Transform _grabPivot;
        
        private GrabInteraction _grabbing;
        private float _initialAngle;
        
        private bool IsGrabbing => _grabbing != null;

// INITIALISATION
        
        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        private void Start() {
            foreach (var interaction in FindObjectsOfType<GrabInteraction>()) {
                interaction.PlayerGrab = this;
            }
        }

// HOLD HANDLING
        
        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public void Grab(GrabInteraction toGrab) {
            SetGrab(toGrab);
            // TODO: Prevent local equilibrium rotation resetting when grabbed... (since _initialAngle - GetAngle() = 0)
            _initialAngle = GetAngle();
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void FixedUpdate() {
            if (!IsGrabbing) return;
            _grabbing.TorsionalOscillator.LocalEquilibriumRotation = _grabbing.TorsionalOscillator.TorqueScale * (_initialAngle - GetAngle());
        }

        private float GetAngle() {
            var plane = _grabbing.transform.parent;
            var f = _cameraTarget.forward; // Camera forward direction
            var p = _cameraTarget.position; // Camera position
            var n = plane.forward; // Plane normal
            var p0 = plane.TransformPoint(Vector3.zero); // A (any) point on the plane
            var parameter = MathsUtilities.MultiplySum(n, p0 - p) / MathsUtilities.MultiplySum(n, f);

            var projectedGrabPosition = p + f * parameter;

            projectedGrabPosition -= plane.position;
            
            // Project _grabPivot position onto _grabbing's parent
            //var projectedGrabPosition = _grabbing.transform.parent.InverseTransformPoint(_grabPivot.position);
            //projectedGrabPosition.z = 0;

            // Trigonometry
            var angle = Mathf.Atan2(projectedGrabPosition.x, projectedGrabPosition.y) * Mathf.Rad2Deg;

            return angle;
        }

        /// <summary>
        /// Release any held object
        /// </summary>
        public void Release() {
            SetGrab(null);
            _grabbing = null;
        }

        /// <summary>
        /// Sets the appropriate components controlling a game objects hold behaviour on or off as appropriate
        /// </summary>
        private void SetGrab(GrabInteraction toGrab) {
            _grabbing = toGrab;
        }
    }
}
