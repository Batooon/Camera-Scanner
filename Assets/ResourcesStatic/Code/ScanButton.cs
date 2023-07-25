using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    [RequireComponent(typeof(Button))]
    public class ScanButton : MonoBehaviour
    {
        private const string ScannerScene = "Scanner";
        private Button _button;

        private void Awake() => 
            _button = GetComponent<Button>();

        private void Start() => 
            _button.onClick.AddListener(OpenScanner);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OpenScanner);

        private void OpenScanner() => 
            SceneLoader.LoadScene(ScannerScene);
    }
}