using SF = UnityEngine.SerializeField;
using UnityEngine;
using Cinemachine;

namespace GameProject.Cameras
{
    [CreateAssetMenu(fileName = "Camera Manager",
	 menuName = "Managers/Camera")]
    public sealed class CameraManager : ScriptableObject
	{
        private Camera _camera = null;
        private Transform _transform = null;
		private CinemachineVirtualCamera _cinemachine = null;
		private CinemachineTransposer _transposer = null;

// PROPERTIES

        public float FieldOfView   => _camera.fieldOfView;
        public Camera    Camera    => _camera;
        public Transform Transform => _transform;

		public Vector3   FollowOffset => _transposer.m_FollowOffset;
        public Transform FollowTarget => _cinemachine.Follow;
        public Transform LookTarget   => _cinemachine.LookAt;

// INITIALISATION

        /// <summary>
        /// Initialises the camera manager
        /// </summary>
        public void Initialise(Camera camera, CinemachineVirtualCamera cinemachine){
			_camera    = camera;
            _transform = camera.transform;
            
            _cinemachine = cinemachine;
			_transposer  = cinemachine.GetCinemachineComponent<CinemachineTransposer>();
        }

        /// <summary>
        /// Finalises the camera manager
        /// </summary>
        public void OnDestroy(){}

// CAMERA MANAGEMENT

        /// <summary>
        /// Changes the camera's field of view
        /// </summary>
        public void SetFOV(float fov){
            _cinemachine.m_Lens.FieldOfView = fov;
        }

        /// <summary>
        /// Changes the camera's look and follow target
        /// <br>Outputs: Cursor screen position, in percent</br>
        /// </summary>
        public void SetTarget(Transform look, Transform follow){
            _cinemachine.LookAt = look;
            _cinemachine.Follow = follow;
        }

        /// <summary>
        /// Changes the camera's follow offset
        /// </summary>
        public void SetFollowOffset(Vector3 offset){
            _transposer.m_FollowOffset = offset;
        }
    }
}