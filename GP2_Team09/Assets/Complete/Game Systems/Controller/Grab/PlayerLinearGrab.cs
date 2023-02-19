using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerLinearGrab : PlayerGrab
    {
        
        private float _initialValue;
        private Vector3 _primaryGrabPos;
        private Vector3 _secondaryGrabPos;
        
// PROPERTIES

        private bool IsGrabbingLinear => _grabbing as LinearGrabInteraction != null;


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
            _initialValue = GetGrabValue().Item1 - ((LinearGrabInteraction)_grabbing).Oscillator.LocalEquilibriumPosition.z;
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingLinear) return;
            var grabValue = GetGrabValue();
            if (grabValue.Item2 == false)
                ((LinearGrabInteraction)_grabbing).Oscillator.LocalEquilibriumPosition = Vector3.forward * (grabValue.Item1 - _initialValue);
        }
        
        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent
        /// </summary>
        private (float, bool) GetGrabValue() {
            var grabbing = (LinearGrabInteraction)_grabbing;
            
            var plane = grabbing.InteractionPlane;
            var altPlane = grabbing.AltInteractionPlane;

            Vector3 projectedGrabPosition = Vector3.zero;

            var dotPrimary = Mathf.Abs(Vector3.Dot(_cameraTarget.forward, plane.forward));
            var dotSecondary = Mathf.Abs(Vector3.Dot(_cameraTarget.forward, altPlane.forward));

            var primaryGrabPos = ProjectedGrabPosition(plane);
            if (primaryGrabPos.Item2) dotPrimary = 0f;
            
            var secondaryGrabPos = ProjectedGrabPosition(altPlane);
            if (secondaryGrabPos.Item2) dotSecondary = 0f;
            
            _primaryGrabPos = primaryGrabPos.Item1;
            _secondaryGrabPos = secondaryGrabPos.Item1;

            var isDivisionByZero = dotPrimary == 0f && dotSecondary == 0f;
            
            if (!isDivisionByZero) {
                projectedGrabPosition = (_primaryGrabPos * dotPrimary + _secondaryGrabPos * dotSecondary)
                                        / (dotPrimary + dotSecondary);
            
                projectedGrabPosition = _grabbing.transform.parent.InverseTransformPoint(projectedGrabPosition);
            }

            return (projectedGrabPosition.z, isDivisionByZero);
        }

        private (Vector3, bool) ProjectedGrabPosition(Transform plane) => MathsUtilities.GetIntersection(new Ray(_cameraTarget.position, _cameraTarget.forward),
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
