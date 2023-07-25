using UnityEngine;
using UnityEngine.UI;

namespace Code.VideoCapture
{
    public class VideoCapture : MonoBehaviour
    {
        [SerializeField] private RawImage _videoOutput;
        [SerializeField] private AspectRatioFitter _fitter;

        private NatcorderCapture _capture;
        private WebCamTexture _outputTexture;

        private void Awake() =>
            _capture = new NatcorderCapture();

        private void Start() => 
            InitializeCamera();

        public void StartRecording() =>
            _capture.StartRecording();

        public void StopRecording() =>
            _capture.StopRecording();

        private void InitializeCamera()
        {
            if (_capture.TryInitializeCamera() == false)
                return;
            
            AdjustOutputTexture();

            _outputTexture = _capture.GetCameraOutput();
            _videoOutput.texture = _outputTexture;
        }

        private void AdjustOutputTexture()
        {
            float ratio = (float)_outputTexture.width / _outputTexture.height;
            _fitter.aspectRatio = ratio;

            float scaleY = _outputTexture.videoVerticallyMirrored ? -1f : 1f;
            _videoOutput.rectTransform.localScale = new Vector3(1f, scaleY, 1f);

            int orient = -_outputTexture.videoRotationAngle;
            _videoOutput.rectTransform.localEulerAngles = new Vector3(0, 0, orient);
        }
    }
}