using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerLinearGrab : PlayerGrab
    {
        private bool IsGrabbingLinear => _grabbing as LinearGrabInteraction != null;

        private float _initialValue;
        
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
            _initialValue = GetGrabValue() - ((LinearGrabInteraction)_grabbing).Oscillator.LocalEquilibriumPosition.z;
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingLinear) return;
            ((LinearGrabInteraction)_grabbing).Oscillator.LocalEquilibriumPosition = Vector3.forward * (GetGrabValue() - _initialValue);
        }
        
        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent
        /// </summary>
        private float GetGrabValue() {
            var plane = _grabbing.InteractionPlane;
            var projectedGrabPosition = ProjectedGrabPosition(plane);
            //projectedGrabPosition = plane.InverseTransformPoint(projectedGrabPosition); // TODO: WHAT IS THIS NEEDED FOR?
            return projectedGrabPosition.z;
        }

        private Vector3 ProjectedGrabPosition(Transform plane) => MathsUtilities.GetIntersection(new Ray(_cameraTarget.position, _cameraTarget.forward),
            new Ray(plane.TransformPoint(Vector3.zero), plane.forward)); 
        
        /// <summary>
        /// Release any held object
        /// </summary>
        public override void Release() {
            base.Release();
        }
        
        /// <summary>
        /// Draw a line indicating the grab positions projection on the plane
        /// </summary>
        protected virtual void OnDrawGizmos() {
            if (!IsGrabbing) return;
            var plane = _grabbing.InteractionPlane;
            var projectedGrabPosition = ProjectedGrabPosition(plane);
            Gizmos.DrawLine(projectedGrabPosition, projectedGrabPosition + plane.forward);
        }
    }
}
