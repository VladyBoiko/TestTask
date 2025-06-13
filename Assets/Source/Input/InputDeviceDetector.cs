using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputDeviceDetector : MonoBehaviour
    {
        public event Action<string> OnDeviceChanged;

        public string CurrentDeviceType { get; private set; }

        private void OnEnable()
        {
            InputSystem.onDeviceChange += OnDeviceChange;
            DetectInitialDevice();
        }

        private void OnDisable()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            if (change == InputDeviceChange.Removed || change == InputDeviceChange.Disconnected)
                return;

            CheckDeviceType(device);
        }

        private void DetectInitialDevice()
        {
            if (Gamepad.current != null)
            {
                CheckDeviceType(Gamepad.current);
            }
            else if (Keyboard.current != null || Mouse.current != null)
            {
                CheckDeviceType(Keyboard.current);
            }
        }

        private void CheckDeviceType(InputDevice device)
        {
            string newType;

            if (device is Gamepad)
            {
                newType = "Gamepad";
                Debug.Log("[Gamepad Connected]");
            }
            else if (device is Keyboard || device is Mouse)
            {
                newType = "Keyboard";
                Debug.Log("[Keyboard/Mouse Connected]");
            }
            else
            {
                newType = "Other";
                Debug.Log($"[Other Device Connected] Device: {device.displayName}");
            }
            
            if (newType != CurrentDeviceType)
            {
                CurrentDeviceType = newType;
                OnDeviceChanged?.Invoke(CurrentDeviceType);
            }
        }
    }
}
