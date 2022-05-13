using CodeBase.Extensions;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class PCInputService : IInputService
    {
        private readonly InputActions _inputSystem;

        public PCInputService()
        {
            _inputSystem = new InputActions();
            EnableInput();
        }

        public Vector3 ViewPointerPosition
        {
            get
            {
                Vector2 position = _inputSystem.PCInput.MouseMovement.ReadValue<Vector2>();
                return Camera.main.ScreenToViewportPoint(position).Clamp01();
            }
        }
        
        public Vector3 WorldPointerPosition
        {
            get
            {
                Vector2 position = _inputSystem.PCInput.MouseMovement.ReadValue<Vector2>();
                return Camera.main.ScreenToWorldPoint(position);
            }
        }

        public void EnableInput() 
            => _inputSystem.PCInput.Enable();

        public void DisableInput() 
            => _inputSystem.PCInput.Disable();
    }
}