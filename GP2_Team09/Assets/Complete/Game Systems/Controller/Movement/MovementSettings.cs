using UnityEngine;

namespace GameProject.Movement
{
    [CreateAssetMenu(fileName = "Movement Settings", menuName = "Settings/Movement Settings")]
    public class MovementSettings : ScriptableObject
    {
// SETTINGS

        [Header("Mouse Look Settings")]
        [SerializeField, Tooltip("The camera turn movement speed, in degrees/s")] 
        private float _turnSensi = 0.75f;

        [SerializeField, Tooltip("The camera tilt movement speed, in degrees/s")]
        private float _lookSensi = 0.75f;

        [SerializeField, Tooltip("The camera min/max look angle, in degrees")]
        private Vector2 _minMaxLookAngle = new Vector2(-90, 80);


        [Header("Movement Settings")]
        [SerializeField, Tooltip("The player horizontal movement speed, in units/s")]
        private float _moveSpeed = 5f;

        [SerializeField, Tooltip("The player horizontal run speed, in units/s")]
        private float _runSpeed = 8f;

        [Header("Crouch")]
        [SerializeField, Tooltip("The transition applied to the height of the character controller and to the vertical position of the character controller when crouching, used as a multiplier to be applied to the default character controller height and as a distance respectively")]
        private AnimationCurve _crouchCurve;
        
        [SerializeField, Tooltip("The transition applied to the height of the character controller and to the vertical position of the character controller when standing, used as a multiplier to be applied to the default character controller height and as a distance respectively")]
        private AnimationCurve _standCurve;

        [Header("Jump")]
        [SerializeField, Tooltip("The initial gradient of the curve is followed until the minimum jump threshold is crossed, at which point the curve is followed")]
        private AnimationCurve _jumpRiseCurve;
        
        [SerializeField, Range(0f, 1f), Tooltip("Minimum time over which the rise curve is followed")]
        private float _riseCancelThreshold = 0.325f;
        
        [SerializeField, Range(0f, 1f), Tooltip("Maximum time before the rise curve is followed")]
        private float _riseMaximumThreshold = 0.75f;
        
        [SerializeField, Tooltip("The time up to which the player may jump after becoming ungrounded")]
        private float _coyoteTime = 0.1f;
        
        [SerializeField, Tooltip("Time over which presses of the jump may be applied retrospectively if conditions for jump are met at a later time")]
        private float _jumpInputBuffer = 0.1f;
        
        [Header("Gravity")]
        [SerializeField, Tooltip("The curve followed until grounded. Once exhausted, the curve's final gradient is followed")]
        private AnimationCurve _fallCurve;
        

// PROPERTIES

        public float TurnSensi => _turnSensi;
        public float LookSensi => _lookSensi;
        public Vector2 MinMaxLookAngle => _minMaxLookAngle;
        
        
        public float MoveSpeed => _moveSpeed;
        public float RunSpeed  => _runSpeed;
        
        public AnimationCurve CrouchCurve => _crouchCurve;
        public AnimationCurve StandCurve => _standCurve;
        
        public AnimationCurve JumpRiseCurve => _jumpRiseCurve;
        public AnimationCurve FallCurve => _fallCurve;
        public float RiseCancelThreshold => _riseCancelThreshold;
        public float RiseMaximumThreshold => _riseMaximumThreshold;
        public float CoyoteTime => _coyoteTime;
        public float JumpInputBuffer => _jumpInputBuffer;
    }
}