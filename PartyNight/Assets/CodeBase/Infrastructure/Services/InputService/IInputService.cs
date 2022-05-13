using UnityEngine;

namespace CodeBase.Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        Vector3 ViewPointerPosition { get; }
        Vector3 WorldPointerPosition { get; }

        void EnableInput();
        void DisableInput();
    }
}