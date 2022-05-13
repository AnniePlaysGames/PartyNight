using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Components
{
    public class MakeChoiceByClick : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ChoiceSide _choiceSide;
        private IDialogueService _dialogueService;

        private void Awake()
        {
            _dialogueService = ServiceLocator.Container.Single<IDialogueService>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _dialogueService.OnMoveToNextCard(_choiceSide);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}