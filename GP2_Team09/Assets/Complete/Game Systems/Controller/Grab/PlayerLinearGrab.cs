using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerLinearGrab : PlayerGrab
    {
        
        private float _initialValue;
        
// PROPERTIES

        private LinearGrabInteraction LinearGrabInteraction => _grabbing as LinearGrabInteraction;
        private bool IsGrabbingLinear => LinearGrabInteraction != null;


// INITIALISATION
        
        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        protected override void Start() {
            foreach (var interaction in FindObjectsOfType<LinearGrabInteraction>()) {
                interaction.PlayerGrab = this;
            }
        }
        
// HOLD HANDLING
        
        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public override void Grab(GrabInteraction toGrab) {
            base.Grab(toGrab);
            if (!IsGrabbingLinear) return;
            _initialValue = GetGrabValue() - LinearGrabInteraction.Oscillator.LocalEquilibriumPosition.z;
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingLinear) return;
            LinearGrabInteraction.Oscillator.LocalEquilibriumPosition = Vector3.forward * (GetGrabValue() - _initialValue);
        }

        /// <summary>
        /// Intersects the line of sight into a perpendicular plane positioned at the movement axis
        /// </summary>
        /// <param name="axis">The axis of movement, represented by the forward vector</param>
        /// <returns>The grab position in world space</returns>
        private Vector3 GetGrabPosition(Transform axis) {
            var ray = new Ray(_cameraTarget.position, _cameraTarget.forward);
            var perpendicularPlane = new Ray(axis.position, - _cameraTarget.forward);
            return MathsUtilities.GetIntersection(ray, perpendicularPlane);
        }
        
        /// <summary>
        /// Gets the local grab position along the forward axis
        /// </summary>
        private float GetGrabValue() {
            var axis = LinearGrabInteraction.MovementAxis;
            var grabPosition = GetGrabPosition(axis);
            grabPosition = axis.InverseTransformPoint(grabPosition);
            return (grabPosition.z);
        }
        
        /// <summary>
        /// Release any held object
        /// </summary>
        public override void Release() {
            base.Release();
        }
        
        /// <summary>
        /// Draw a line indicating the grab positions projection on the plane
        /// </summary>
        private void OnDrawGizmos() {
            if (!IsGrabbing) return;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(GetGrabPosition(LinearGrabInteraction.MovementAxis), 0.1f);
        }
    }
}
