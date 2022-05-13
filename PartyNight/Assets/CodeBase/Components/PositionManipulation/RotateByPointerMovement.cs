using CodeBase.Extensions;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.InputService;
using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Components.PositionManipulation
{
    public class RotateByPointerMovement : MonoBehaviour
    {
        private const float XValueOfCenter = 0.5f;
        
        [SerializeField] private float _offsetAngle;
        [SerializeField] private float _rotationCoefficient;
        [Range(0, 180)] [SerializeField] private float _maxAngleInDegrees;

        private IInputService _inputService;

        private void Awake()
            => _inputService = ServiceLocator.Container.Single<IInputService>();

        private void Update()
        {
            float rotationAngle = AngleFromPosition(_inputService.ViewPointerPosition.Clamp01());
            rotationAngle -= _offsetAngle;
            rotationAngle = math.clamp(rotationAngle, -_maxAngleInDegrees, _maxAngleInDegrees);
            Rotate(rotationAngle);
        }

        private float AngleFromPosition(Vector3 position)
            => (position.x - XValueOfCenter) * _rotationCoefficient;
        
        private void Rotate(float rotationAngle)
            => transform.eulerAngles = new Vector3(0, 0, -rotationAngle);
    }
}