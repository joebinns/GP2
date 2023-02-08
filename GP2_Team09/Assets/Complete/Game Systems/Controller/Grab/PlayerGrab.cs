using System;
using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerGrab : MonoBehaviour
    {
        [SF] private Transform _cameraTarget;
        
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
            _initialAngle = GetAngle() + _grabbing.TorsionalOscillator.LocalEquilibriumRotation.z;
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void FixedUpdate() {
            if (!IsGrabbing) return;
            _grabbing.TorsionalOscillator.LocalEquilibriumRotation = Vector3.back * (GetAngle() - _initialAngle);
        }

        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent, from which the angle from to the pivot is determined
        /// </summary>
        private float GetAngle() {
            var plane = _grabbing.transform.parent;
            var projectedGrabPosition = MathsUtilities.GetIntersection(new Ray(_cameraTarget.position, _cameraTarget.forward),
                new Ray(plane.TransformPoint(Vector3.zero), plane.forward));
            projectedGrabPosition = plane.InverseTransformPoint(projectedGrabPosition);
            var angle = Mathf.Atan2(projectedGrabPosition.x, projectedGrabPosition.y) * Mathf.Rad2Deg;
            return angle;
        }

        /// <summary>
        /// Release any held object
        /// </summary>
        public void Release() {
            SetGrab(null);
        }

        /// <summary>
        /// Sets the appropriate components controlling a game objects hold behaviour on or off as appropriate
        /// </summary>
        private void SetGrab(GrabInteraction toGrab) {
            _grabbing = toGrab;
        }

        /// <summary>
        /// Draw a line indicating the grab positions projection on the plane
        /// </summary>
        private void OnDrawGizmos() {
            if (!IsGrabbing) return;
            var plane = _grabbing.transform.parent;
            var projectedGrabPosition = MathsUtilities.GetIntersection(new Ray(_cameraTarget.position, _cameraTarget.forward),
                new Ray(plane.TransformPoint(Vector3.zero), plane.forward));
            Gizmos.DrawLine(projectedGrabPosition, projectedGrabPosition + plane.forward);
        }
    }
}
