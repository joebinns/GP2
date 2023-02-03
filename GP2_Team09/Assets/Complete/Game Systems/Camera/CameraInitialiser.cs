using SF = UnityEngine.SerializeField;
using UnityEngine;
using Cinemachine;

namespace GameProject.Cameras
{
	[AddComponentMenu("Managers/Camera Initialiser"), 
	 DefaultExecutionOrder(-1), DisallowMultipleComponent]
	public class CameraInitialiser : MonoBehaviour
	{
		[SF] private Camera _mainCamera = null;
		[SF] private CinemachineVirtualCamera _cinemachine = null;
		[SF] private CameraManager _manager = null;
		
		private static CameraInitialiser s_active = null;

// INITIALISATION

        /// <summary>
        /// Initialises the camera manager
        /// </summary>
        private void Awake(){
			if (s_active == null){
				_manager.Initialise(_mainCamera, _cinemachine);
				s_active = this;

            } else Destroy(this);
		}

        /// <summary>
        /// Deinitialises the camera manager
        /// </summary>
        private void OnDestroy(){
			if (s_active != this) return;
			_manager.OnDestroy();
		}
	}
}