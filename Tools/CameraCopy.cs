using UnityEngine;

namespace VV.Utility.Tools
{
    [RequireComponent(typeof(UnityEngine.Camera))   ]
    public class CameraCopy : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera cameraToCopy;
        [SerializeField] private bool copyFOV = false;

        private UnityEngine.Camera currentCamera = null;

        private void Awake()
        {
            currentCamera = GetComponent<UnityEngine.Camera>();
        }

        private void Update()
        {
            if (cameraToCopy == null) return;

            if (copyFOV) currentCamera.fieldOfView = cameraToCopy.fieldOfView;
        }
    }
}