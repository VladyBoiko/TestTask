using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using UnityEngine.UI;

namespace Menu
{
    public class InputHintsCanvasController : MonoBehaviour
    {
        [Header("Input Hints")] 
        [SerializeField] private GameObject _keyboardHints;
        [SerializeField] private GameObject _xboxGamepadHints;
        [SerializeField] private GameObject _playstationGamepadHints;
        
        [Header("Input Device Detector")]
        [SerializeField] private InputDeviceDetector _inputDeviceDetector;
        
        private void Awake()
        {
            _playstationGamepadHints.SetActive(false);
            _xboxGamepadHints.SetActive(false);
            _keyboardHints.SetActive(false);
        }

        private void OnEnable()
        {
            SetIcons(_inputDeviceDetector.CurrentDeviceType);
            _inputDeviceDetector.OnDeviceChanged += HandleDeviceChanged;
        }
        
        private void OnDisable()
        {
            _inputDeviceDetector.OnDeviceChanged -= HandleDeviceChanged;
        }

        void HandleDeviceChanged(string type)
        {
            SetIcons(type);
        }
        
        private void SetIcons(string type)
        {
            _playstationGamepadHints.SetActive(false);
            _xboxGamepadHints.SetActive(false);
            _keyboardHints.SetActive(false);
            
            switch (type)
            {
                case "Gamepad":
                    var gamepad = Gamepad.current;
                    if (gamepad != null)
                    {
                        CursorDisable();
                        
                        switch (gamepad)
                        {
                            case DualShockGamepad:
                                Debug.Log("[DualShock Gamepad Detected]");
                                _playstationGamepadHints.SetActive(true);
                                break;
                            case XInputController:
                                Debug.Log("[Xbox Gamepad Detected]");
                                _xboxGamepadHints.SetActive(true);
                                break;
                            default:
                                Debug.LogWarning("Unknown gamepad type, using default hints.");
                                _xboxGamepadHints.SetActive(true);
                                break;
                        }
                    }
                    break;
                case "Keyboard":
                    CursorEnable();
                    
                    _keyboardHints.SetActive(true);
                    break;
            }
        }
        
        public void CursorEnable()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        public void CursorDisable()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}