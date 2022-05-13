using System.Collections.Generic;
using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using CodeBase.Infrastructure.Services.Dialogues.Factory;
using CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Stats;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues
{
    public class DialogueService : IDialogueService
    {
        private readonly IUIFactory _uiFactory;
        private readonly IStatsService _statsService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistantProgressService _progressService;
        private readonly ICardPackFactory _cardPackFactory;
        private readonly Dictionary<string, DialogueCard> _cardById = new Dictionary<string, DialogueCard>();
        private PlayerProgress _playerProgress;
        private DialogueWindow _dialogueWindow;
        private List<Dialogue> _dialoguePack;
        private DialogueCard _currentDialogueCard;
        private int _currentDialogueIndex;

        public DialogueService(IUIFactory uiFactory, IStatsService statsService, ISaveLoadService saveLoadService, IPersistantProgressService progressService)
        {
            _uiFactory = uiFactory;
            _statsService = statsService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _cardPackFactory = new CardPackFactory();
        }

        public void InitDialogues()
        {
            _dialogueWindow = _uiFactory.DialogueWindow;
            _dialoguePack = _cardPackFactory.GeneratePack();
            _playerProgress = _progressService.PlayerProgress;
            StartDialogue(_playerProgress.dialogueIndex);
        }
        
        private void StartDialogue(int index)
        {
            _currentDialogueIndex = index;
            _cardById.Clear();
            List<DialogueCard> currentDialogue = _dialoguePack[index].DialogueCards;
            foreach (DialogueCard card in currentDialogue)
            {
                _cardById.Add(card.ID,card);
            }
            _currentDialogueCard = currentDialogue[_playerProgress.cardIndex];
            _dialogueWindow.SetIndicatorValues(_statsService.CurrentData);
            _dialogueWindow.UpdateCardView(_currentDialogueCard);
        }

        public void OnMoveToNextCard(ChoiceSide side)
        {
            ChoiceData.ChoiceData playerChoice = _currentDialogueCard.ChoiceElementsData[(int) side];
            string nextCardId = playerChoice.ConnectedNodeId;
            
            _progressService.PlayerProgress.cardIndex++;
            _statsService.UpdateCurrentData(playerChoice.Impact);
            _dialogueWindow.SetIndicatorValues(_statsService.CurrentData);

            MoveToNextCardOrDialogue(nextCardId);
        }

        private void MoveToNextCardOrDialogue(string nextCardId)
        {
            if (string.IsNullOrEmpty(nextCardId))
            {
                MoveToNextDialogue();
            }
            else
            {
                _currentDialogueCard = _cardById[nextCardId];
                _dialogueWindow.UpdateCardView(_currentDialogueCard);
            }
        }

        private void MoveToNextDialogue()
        {
            if (_currentDialogueIndex < _dialoguePack.Count - 1)
            {
                SaveProgressBetweenDialogues();
                StartDialogue(_currentDialogueIndex + 1);
            }
            else
            {
                Debug.Log("Dialogue Finished");
            }
        }
        
        private void SaveProgressBetweenDialogues()
        {
            _progressService.PlayerProgress.cardIndex = 0;
            _progressService.PlayerProgress.dialogueIndex++;
            _statsService.UpdateProgress(_playerProgress);

            _saveLoadService.SaveProgress();
        }
    }
}