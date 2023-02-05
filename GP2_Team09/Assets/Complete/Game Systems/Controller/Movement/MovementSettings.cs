using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Movement
{
    [CreateAssetMenu(fileName = "Movement Settings",
     menuName = "Settings/Movement Settings")]
    public class MovementSettings : ScriptableObject
    {
// SETTINGS

        [Header("Mouse Look Settings")]
        [SF, Tooltip("The camera turn movement speed, in degrees/s")] 
        private float _turnSensi = 0.75f;

        [SF, Tooltip("The camera tilt movement speed, in degrees/s")]
        private float _lookSensi = 0.75f;

        [SF, Tooltip("The camera min/max look angle, in degrees")]
        private Vector2 _minMaxLookAngle = new Vector2(-90, 80);


        [Header("Movement Settings")]
        [SF, Tooltip("The player horizontal movement speed, in distance/s")]
        private float _moveSpeed = 10f;
        
        [Header("Crouch")]
        [SF, Tooltip("The transition applied to the height of the character controller and to the vertical position of the character controller when crouching, used as a multiplier to be applied to the default character controller height and as a distance respectively")]
        private AnimationCurve _crouchCurve;
        
        [SF, Tooltip("The transition applied to the height of the character controller and to the vertical position of the character controller when standing, used as a multiplier to be applied to the default character controller height and as a distance respectively")]
        private AnimationCurve _standCurve;

        [Header("Jump")]
        [SF, Tooltip("")]
        private AnimationCurve _jumpRiseCurve;
        
        [Header("Gravity")]
        [SF, Tooltip("")]
        private AnimationCurve _fallCurve;
        

// PROPERTIES

        public float TurnSensi => _turnSensi;
        public float LookSensi => _lookSensi;
        public Vector2 MinMaxLookAngle => _minMaxLookAngle;
        
        public float MoveSpeed => _moveSpeed;
        public AnimationCurve CrouchCurve => _crouchCurve;
        public AnimationCurve StandCurve => _standCurve;
        public AnimationCurve JumpRiseCurve => _jumpRiseCurve;
        public AnimationCurve FallCurve => _fallCurve;
    }
}