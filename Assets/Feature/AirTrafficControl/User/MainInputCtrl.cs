using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hsinpa.UserInput {
    public class MainInputCtrl
    {
        GeneralInputSetting generalInputSetting;

        public delegate void MouseActionDelegate(Vector3 screenPosition, Vector3 worldPosition);

        public MouseActionDelegate OnTouchDownEvent;
        public MouseActionDelegate OnTouchUpEvent;
        public MouseActionDelegate OnPressEvent;

        private Camera m_camera;
        
        public MainInputCtrl(Camera camera)
        {
            m_camera = camera;
            generalInputSetting = new GeneralInputSetting();
            generalInputSetting.Enable();

            generalInputSetting.PlayerInput.Touch.performed += OnTouchDown;
            generalInputSetting.PlayerInput.Touch.canceled += OnTouchUp;
            generalInputSetting.PlayerInput.Press.performed += OnPress;
        }

        void OnPress(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            Debug.Log("On Press");

            TriggerMouseInput(OnPressEvent);
        }

        void OnTouchDown(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext) {
            Debug.Log("On Touch Down");
            TriggerMouseInput(OnTouchDownEvent);
        }

        void OnTouchUp(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            Debug.Log("On Touch Up");
            TriggerMouseInput(OnTouchUpEvent);
        }

        private void TriggerMouseInput(MouseActionDelegate mouseActionDelegate) {
            Vector3 screenPosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = m_camera.ScreenToWorldPoint(screenPosition);

            mouseActionDelegate?.Invoke(screenPosition, worldPosition);
        }

        void Dispose() {
            if (generalInputSetting != null) {
                generalInputSetting.PlayerInput.Touch.performed -= OnTouchDown;
                generalInputSetting.PlayerInput.Touch.canceled -= OnTouchUp;
                generalInputSetting = null;
            }
        }

    }
}
