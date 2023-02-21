using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerTorsionalGrab : PlayerGrab
    {
        
        private float _angle = 0f;
        
// PROPERTIES

        private TorsionalGrabInteraction TorsionalGrabInteraction => _grabbing as TorsionalGrabInteraction;
        private bool IsGrabbingTorsional => TorsionalGrabInteraction != null;

// INITIALISATION
        
        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        protected override void Start() {
            foreach (var interaction in FindObjectsOfType<TorsionalGrabInteraction>()) {
                interaction.PlayerGrab = this;
            }
        }
        
// HOLD HANDLING

        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public override void Grab(GrabInteraction toGrab) {
            base.Grab(toGrab);
            if (!IsGrabbingTorsional) return;
            _angle = MathsUtilities.AngleRepresentation(GetAngle());
        }
        
        /// <summary>
        /// Sets the appropriate components controlling a game objects hold behaviour on or off as appropriate
        /// </summary>
        protected override void SetGrab(GrabInteraction toGrab) {
            var shouldGrab = toGrab != null;
            var grabbing = (shouldGrab ? toGrab : _grabbing) as TorsionalGrabInteraction;
            if (grabbing == null) return;
            
            grabbing.TorsionalOscillator.enabled = shouldGrab;

            _grabbing = toGrab;
        }

        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingTorsional) return;
            var deltaAngle = UpdateAngle();
            TorsionalGrabInteraction.AdjustTargetRotation(-deltaAngle);
        }

        /// <summary>
        /// </summary>
        /// <returns>Delta angle, without clamping</returns>
        private float UpdateAngle() {
            var angle = GetAngle();
            var deltaAngle = MathsUtilities.GetDeltaAngle(ref angle, _angle);
            _angle = angle;
            return deltaAngle;
        }

        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent, from which the angle from to the pivot is determined
        /// </summary>
        private float GetAngle() {
            var axis = TorsionalGrabInteraction.RotationAxis;
            var ray = new Ray(_cameraTarget.position, _cameraTarget.forward);
            var perpendicularPlane = new Ray(axis.position, - _cameraTarget.forward);
            var grabPosition = Vector3.zero;
            var angle = 0f;
            
            if (Physics.Raycast(ray, out var hitInfo, (ray.origin - axis.position).magnitude, LayerMask.GetMask("Interaction")))
                grabPosition = hitInfo.point;
            else
                grabPosition = MathsUtilities.GetIntersection(ray, perpendicularPlane); //MathsUtilities.GetClosestPoint(ray, plane.position);
            
            grabPosition = axis.InverseTransformPoint(grabPosition);
            angle = Mathf.Atan2(grabPosition.x, grabPosition.y) * Mathf.Rad2Deg;
            return angle;
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
        protected virtual void OnDrawGizmos() {
            if (!IsGrabbing) return;
            var closestPoint = MathsUtilities.GetClosestPoint(new Ray(_cameraTarget.position, _cameraTarget.forward),
                TorsionalGrabInteraction.RotationAxis.position);
            Gizmos.DrawWireSphere(TorsionalGrabInteraction.RotationAxis.position, 0.1f);
            Gizmos.DrawRay(_cameraTarget.position, _cameraTarget.forward * 100f);
            Gizmos.DrawLine(closestPoint, TorsionalGrabInteraction.RotationAxis.position);
            Gizmos.DrawSphere(closestPoint, 0.1f);
        }
    }
}
