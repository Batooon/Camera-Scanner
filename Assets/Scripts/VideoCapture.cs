using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class VideoCapture : MonoBehaviour
    {
        [SerializeField] private RawImage _videoOutput;
        [SerializeField] private AspectRatioFitter _fitter;
        
        private bool _cameraAvailable;
        private WebCamTexture _rearCamera;
        private Texture _defaultBackground;

        private void Start()
        {
            _defaultBackground = _videoOutput.texture;
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("no camera found");
                _cameraAvailable = false;
                return;
            }

            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].isFrontFacing == false)
                {
                    _rearCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);
                }
            }

            if (_rearCamera == null)
            {
                Debug.Log("unable to find rear camera");
                return;
            }
            
            _rearCamera.Play();
            _videoOutput.texture = _rearCamera;
            _cameraAvailable = true;
        }

        private void Update()
        {
            if (_cameraAvailable == false)
                return;

            float ratio = (float)_rearCamera.width / _rearCamera.height;
            _fitter.aspectRatio = ratio;

            float scaleY = _rearCamera.videoVerticallyMirrored ? -1f : 1f;
            _videoOutput.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -_rearCamera.videoRotationAngle;
            _videoOutput.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }
}