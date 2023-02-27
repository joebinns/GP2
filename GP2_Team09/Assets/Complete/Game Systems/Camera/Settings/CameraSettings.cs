using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Cameras
{
    [CreateAssetMenu(fileName = "Camera Settings",
     menuName = "Settings/Camera Settings")]
    public class CameraSettings : ScriptableObject
    {
        [Header("Field of View Settings")]
        [SF] private float _defaultFOV = 60f;
        [SF] private float _maxZoomFOV = 30f;
        [SF] private AnimationCurve _zoomInCurve  = null;
        [SF] private AnimationCurve _zoomOutCurve = null;

        public float DefaultFOV => _defaultFOV;
        public float MaxZoomFOV => _maxZoomFOV;
        public AnimationCurve ZoomOutCurve => _zoomInCurve;
        public AnimationCurve ZoomInCurve  => _zoomOutCurve;
    }
}