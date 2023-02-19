using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public class PlayerTorsionalGrab : PlayerGrab
    {
        private bool IsGrabbingTorsional => _grabbing as TorsionalGrabInteraction != null;
        
        private float _initialAngle;

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
            _initialAngle = GetAngle() + ((TorsionalGrabInteraction)_grabbing).RotationAxis.InverseTransformDirection(_grabbing.transform.TransformDirection(((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation)).z;
        }
        
        /*
        private float _angle = 0f;

        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingTorsional) return;
            var grabbing = ((TorsionalGrabInteraction)_grabbing);

            var angle = ClockfaceRepresentation(GetAngle());

            if (_angle < 0f)
                angle = -Mathf.Abs(angle);
            else
                angle = Mathf.Abs(angle);

            float deltaAngle = angle - _angle;
            
            //if (_angle > 0f)
            //    deltaAngle = Mathf.Abs(deltaAngle);
                
            //Debug.Log("_angle " + _angle);
            //Debug.Log("angle " + angle);
            //Debug.Log(deltaAngle);
            
            
            

            
            
            //((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation = - _grabbing.transform.InverseTransformDirection(_grabbing.InteractionPlane.forward) * (angle - _initialAngle);
            grabbing.AdjustEquilibrium(- _grabbing.transform.InverseTransformDirection(_grabbing.InteractionPlane.forward) * deltaAngle);
            
            _angle = angle;
            
            
            
            //var angle = ((TorsionalGrabInteraction)_grabbing).UseLimits ? Mathf.Clamp(ClockfaceRepresentation(GetAngle()), ((TorsionalGrabInteraction)_grabbing).MinAngle, ((TorsionalGrabInteraction)_grabbing).MaxAngle) : GetAngle();
            //((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation = - _grabbing.transform.InverseTransformDirection(_grabbing.InteractionPlane.forward) * (angle - _initialAngle);
        }
        
        /// <summary>
        /// Returns the proper rotation
        /// </summary>
        private float ClockfaceRepresentation(float unityRepresentation) => unityRepresentation < 0f ? 360f + unityRepresentation : unityRepresentation;
        */
        
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingTorsional) return;
            ((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation = - _grabbing.transform.InverseTransformDirection(((TorsionalGrabInteraction)_grabbing).RotationAxis.forward) * (GetAngle() - _initialAngle);
        }
         
        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent, from which the angle from to the pivot is determined
        /// </summary>
        private float GetAngle() {
            var plane = ((TorsionalGrabInteraction)_grabbing).RotationAxis;
            var ray = new Ray(_cameraTarget.position, _cameraTarget.forward);
            var perpendicularPlane = new Ray(plane.position, - _cameraTarget.forward);
            var grabPosition = Vector3.zero;
            var angle = 0f;
            
            if (Physics.Raycast(ray, out var hitInfo, (ray.origin - plane.position).magnitude, LayerMask.GetMask("Interaction")))
                grabPosition = hitInfo.point;
            else
                grabPosition = MathsUtilities.GetIntersection(ray, perpendicularPlane).Item1; //MathsUtilities.GetClosestPoint(ray, plane.position);
            
            grabPosition = plane.InverseTransformPoint(grabPosition);
            angle = Mathf.Atan2(grabPosition.x, grabPosition.y) * Mathf.Rad2Deg;
            return angle;
        }
        
        /// <summary>
        /// Release any held object
        /// </summary>
        public override void Release() {
            base.Release();
        }
        

        /*
        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public override void Grab(GrabInteraction toGrab) {
            base.Grab(toGrab);
            if (!IsGrabbingTorsional) return;
            _initialAngle = GetAngle() + ((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation.z;
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void Update() {
            if (!IsGrabbing) return;
            if (!IsGrabbingTorsional) return;
            ((TorsionalGrabInteraction)_grabbing).TorsionalOscillator.LocalEquilibriumRotation = Vector3.back * (GetAngle() - _initialAngle);
        }
        
        /// <summary>
        /// Intersects the line of sight into the plane represented by the normal of the grabbed objects parent, from which the angle from to the pivot is determined
        /// </summary>
        private float GetAngle() {
            var plane = _grabbing.InteractionPlane;
            var projectedGrabPosition = ProjectedGrabPosition(plane);
            projectedGrabPosition = plane.InverseTransformPoint(projectedGrabPosition);
            var angle = Mathf.Atan2(projectedGrabPosition.x, projectedGrabPosition.y) * Mathf.Rad2Deg;
            return angle;
        }

        private Vector3 ProjectedGrabPosition(Transform plane) => MathsUtilities.GetIntersection(new Ray(_cameraTarget.position, _cameraTarget.forward),
            new Ray(plane.TransformPoint(Vector3.zero), plane.forward)); 
        
        /// <summary>
        /// Draw a line indicating the grab positions projection on the plane
        /// </summary>
        protected virtual void OnDrawGizmos() {
            if (!IsGrabbing) return;
            var plane = _grabbing.InteractionPlane;
            var projectedGrabPosition = ProjectedGrabPosition(plane);
            Gizmos.DrawLine(projectedGrabPosition, projectedGrabPosition + plane.forward);
        }
        */
        
        
        /// <summary>
        /// Draw a line indicating the grab positions projection on the plane
        /// </summary>
        protected virtual void OnDrawGizmos() {
            if (!IsGrabbing) return;
            var closestPoint = MathsUtilities.GetClosestPoint(new Ray(_cameraTarget.position, _cameraTarget.forward),
                ((TorsionalGrabInteraction)_grabbing).RotationAxis.position);
            Gizmos.DrawWireSphere(((TorsionalGrabInteraction)_grabbing).RotationAxis.position, 0.1f);
            Gizmos.DrawRay(_cameraTarget.position, _cameraTarget.forward * 100f);
            Gizmos.DrawLine(closestPoint, ((TorsionalGrabInteraction)_grabbing).RotationAxis.position);
            Gizmos.DrawSphere(closestPoint, 0.1f);
        }
    }
}
