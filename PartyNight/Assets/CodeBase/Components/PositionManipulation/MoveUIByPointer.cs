using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Components.PositionManipulation
{
    public class MoveUIByPointer : MonoBehaviour
    {
        [SerializeField] private int _maxOffsetX = 200;
        [SerializeField] private int _maxOffsetY = 200;
        [SerializeField] private MoveType _moveType;

        private IInputService _inputService;
        private RectTransform _uiRoot;
        private RectTransform _uiElement;
        private Vector2 _initialPosition;

        private void Awake()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
            _uiRoot = ServiceLocator.Container.Single<IUIFactory>().UIRoot;
            _uiElement = GetComponent<RectTransform>();
            _initialPosition = _uiElement.anchoredPosition;
        }

        public void Update()
        {
            Vector3 viewportPosition = _inputService.ViewPointerPosition;
            Vector2 sizeDelta = _uiRoot.sizeDelta;
            Vector2 anchoredPosition = ViewPortToAnchoredPosition(viewportPosition, sizeDelta);
            _uiElement.anchoredPosition = SetRange(ApplyMoveTypeSettings(anchoredPosition));
        }

        private static Vector2 ViewPortToAnchoredPosition(Vector3 viewportPosition, Vector2 sizeDelta)
        {
            Vector2 newAnchoredPosition = new Vector2(
                ((viewportPosition.x * sizeDelta.x) - (sizeDelta.x * 0.5f)),
                ((viewportPosition.y * sizeDelta.y) - (sizeDelta.y * 0.5f)));
            return newAnchoredPosition;
        }

        private Vector2 SetRange(Vector2 position)
        {
            float clampX = Mathf.Clamp(position.x, _initialPosition.x -_maxOffsetX, _initialPosition.x + _maxOffsetX);
            float clampY = Mathf.Clamp(position.x, _initialPosition.y -_maxOffsetY, _initialPosition.y + _maxOffsetY);
            return new Vector2(clampX, clampY);
        }

        private Vector2 ApplyMoveTypeSettings(Vector2 anchoredPosition)
        {
            switch (_moveType)
            {
                case MoveType.Horizontal:
                    anchoredPosition.y = _uiElement.anchoredPosition.y;
                    break;
                case MoveType.Vertical:
                    anchoredPosition.x = _uiElement.anchoredPosition.x;
                    break;
                case MoveType.Both:
                    break;
            }
            return anchoredPosition;
        }
    }
}