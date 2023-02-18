using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerLinearGrab : PlayerGrab
    {
        private bool IsGrabbingLinear => _grabbing as LinearGrabInteraction != null;

        private float _initialValue;

        private Vector3 _primaryGrabPos;
        private Vector3 _secondaryGrabPos;
        
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
            // TODO: Check which plane has the correct dot product to line of sight
            var plane = _grabbing.InteractionPlane;
            var altPlane = ((LinearGrabInteraction)_grabbing).AltInteractionPlane;

            Vector3 projectedGrabPosition;
            
            /*
            if (Mathf.Abs(Vector3.Dot(_cameraTarget.forward, plane.forward)) > Mathf.Abs(Vector3.Dot(_cameraTarget.forward, altPlane.forward)))
                projectedGrabPosition = ProjectedGrabPosition(plane);
            else 
                projectedGrabPosition = ProjectedGrabPosition(altPlane);
            */

            var dotPrimary = Mathf.Abs(Vector3.Dot(_cameraTarget.forward, plane.forward));
            var dotSecondary = Mathf.Abs(Vector3.Dot(_cameraTarget.forward, altPlane.forward));

            _primaryGrabPos = ProjectedGrabPosition(plane);
            _secondaryGrabPos = ProjectedGrabPosition(altPlane);
            
            //projectedGrabPosition = ProjectedGrabPosition(plane) * Mathf.Abs(Vector3.Dot(_cameraTarget.forward, plane.forward)) 
            //                        + ProjectedGrabPosition(altPlane) * Mathf.Abs(Vector3.Dot(_cameraTarget.forward, altPlane.forward));

            Debug.Log("pri " + dotPrimary);
            Debug.Log("sec " + dotSecondary);
            
            // TODO: PREVENT GRAB POSITIONS WHICH ARE 'BACKWARDS'

            projectedGrabPosition = (_primaryGrabPos * dotPrimary + _secondaryGrabPos * dotSecondary)
                                    / (dotPrimary + dotSecondary);
            
            projectedGrabPosition = _grabbing.transform.parent.InverseTransformPoint(projectedGrabPosition);
            
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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_primaryGrabPos, 0.2f);
            Gizmos.DrawWireSphere(_secondaryGrabPos, 0.15f);
        }
    }
}
