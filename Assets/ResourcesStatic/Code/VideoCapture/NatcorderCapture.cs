using System.Threading.Tasks;
using NatML.Recorders;
using NatML.Recorders.Clocks;
using UnityEngine;

namespace Code.VideoCapture
{
    public class NatcorderCapture
    {
        private bool _cameraAvailable;
        private WebCamTexture _rearCamera;

        private MP4Recorder _recorder;
        private bool _recording;

        public bool TryInitializeCamera() =>
            TryFindRearCamera();

        public WebCamTexture GetCameraOutput()
        {
            _rearCamera.Play();
            Debug.Log(_rearCamera);
            return _rearCamera;
        }

        public async void StartRecording()
        {
            _recorder = new MP4Recorder(_rearCamera.width, _rearCamera.height, 30);

            _recording = true;
            var clock = new RealtimeClock();
            while (_recording)
            {
                _recorder.CommitFrame(_rearCamera.GetPixels32(), clock.timestamp);

                await Task.Yield();
            }
        }

        public async void StopRecording()
        {
            _recording = false;

            var recordingPath = await _recorder.FinishWriting();
            NativeGallery.Permission permission = NativeGallery.SaveVideoToGallery(recordingPath, "Camera Test",
                "testvideo.mp4", (success, path) => Debug.Log($"Media save result: {success} {path}"));
            Debug.Log($"permission result: {permission}");
        }

        private bool TryFindRearCamera()
        {
            WebCamDevice[] devices = WebCamTexture.devices;

            if (devices.Length == 0)
            {
                Debug.Log("no camera found");
                _cameraAvailable = false;
                return false;
            }

            foreach (var cameraDevice in devices)
            {
                if (cameraDevice.isFrontFacing == false)
                {
                    Debug.Log("rear camera found");
                    _rearCamera = new WebCamTexture();
                }
            }

            if (_rearCamera == null)
            {
                Debug.Log("unable to find rear camera");
                _cameraAvailable = false;
                return false;
            }

            _cameraAvailable = true;
            Debug.Log("camera found and initialized");

            return _cameraAvailable;
        }
    }
}