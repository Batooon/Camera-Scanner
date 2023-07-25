using Niantic.ARDK.Extensions.Scanning;
using UnityEngine;

namespace Code.Scanner
{
    public class Scan : MonoBehaviour
    {
        [SerializeField] private ARScanManager _scanManager;

        private GameObject _scannedObject;

        private void Start() =>
            _scanManager.SetScanTargetId("0");

        public void ScanButtonPressed()
        {
            Debug.Log("starting scanning");
            _scanManager.StartScanning();
        }

        public void SaveButtonPressed()
        {
            Debug.Log("saving scan");
            _scanManager.StopScanning();
            _scanManager.UploadScan("0", (progress) => { Debug.Log($"uploading progress: {progress}"); },
                (success, error) => { Debug.Log($"saving scan: {success}, {error}"); });
            _scanManager.StartProcessing();
        }
    }
}