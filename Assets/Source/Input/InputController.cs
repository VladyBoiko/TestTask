using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        public event Action<Vector2> OnNavigate;
        public event Action OnSubmit;
        public event Action OnCancel;
        
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset _inputActionAsset;
        
        [Header("Input Action names")]
        [SerializeField] private InputConfig _inputConfig;
        
        private InputActionMap _uiInputActionMap;
        
        private InputAction _submitAction;
        private InputAction _cancelAction;
        private InputAction _navigateAction;
        
        private void Awake()
        {
            _inputActionAsset.Enable();
            
            _uiInputActionMap = _inputActionAsset.FindActionMap(_inputConfig.ActionMapName);
            
            _submitAction = _uiInputActionMap.FindAction(_inputConfig.SubmitActionName);
            _cancelAction = _uiInputActionMap.FindAction(_inputConfig.CancelActionName);
            _navigateAction = _uiInputActionMap.FindAction(_inputConfig.NavigateActionName);   
        }
        
        private void OnEnable()
        {
            _uiInputActionMap.Enable();
    
            _submitAction.performed += OnSubmitInput;
            _cancelAction.performed += OnCancelInput;
            _navigateAction.performed += OnNavigateInput;
            _navigateAction.canceled += OnNavigateInput;
        }

        private void OnDisable()
        {
            _uiInputActionMap.Disable();
    
            _submitAction.performed -= OnSubmitInput;
            _cancelAction.performed -= OnCancelInput;
            _navigateAction.performed -= OnNavigateInput;
            _navigateAction.canceled -= OnNavigateInput;
        }
        
        private void OnSubmitInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("Submit pressed");
            OnSubmit?.Invoke();
        }

        private void OnCancelInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("Cancel pressed");
            OnCancel?.Invoke();
        }

        private void OnNavigateInput(InputAction.CallbackContext ctx)
        {
            Vector2 navigation = ctx.ReadValue<Vector2>();
            Debug.Log($"Navigate: {navigation}");
            OnNavigate?.Invoke(navigation);
        }
    }
}