using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;

namespace Deege.Game
{

    public class ControllerDetector : MonoBehaviour
    {

        private void OnEnable()
        {
            // Subscribe to the device change event
            InputSystem.onDeviceChange += OnDeviceChange;
            DetectController();
        }

        private void OnDisable()
        {
            // Always remember to unsubscribe from the event to avoid memory leaks or unwanted behavior
            InputSystem.onDeviceChange -= OnDeviceChange;
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            DetectController();
            switch (change)
            {
                case InputDeviceChange.Added:
                    // Device got added
                    Debug.Log("Device added: " + device.displayName);
                    break;
                case InputDeviceChange.Removed:
                    // Device got removed
                    Debug.Log("Device removed: " + device.displayName);
                    break;
                case InputDeviceChange.Disconnected:
                    // Device got unplugged
                    Debug.Log("Device disconnected: " + device.displayName);
                    break;
                case InputDeviceChange.Reconnected:
                    // Device got re-plugged in
                    Debug.Log("Device reconnected: " + device.displayName);
                    break;
                    // Handle other cases if necessary
            }
        }

        private void DetectController()
        {
            foreach (var gamepad in Gamepad.all)
            {
                if (gamepad is DualShockGamepad)
                {
                    Debug.Log(gamepad.displayName + " is a PlayStation controller.");
                }
                else if (gamepad is XInputController)
                {
                    Debug.Log(gamepad.displayName + " is an Xbox controller.");
                }
                else
                {
                    Debug.Log(gamepad.displayName + " is a gamepad of another type.");
                }
            }
        }
    }
}