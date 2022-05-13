using System.Collections.Generic;
using CodeBase.Components;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public class DialogueWindow : BaseWindow
    {
        [SerializeField] private GameObject _indicatorPanel;
        [SerializeField] private Image _portrait;
        [SerializeField] private TextMeshProUGUI _characterName;
        [SerializeField] private TextMeshProUGUI _characterPhrase;
        [SerializeField] private TextMeshProUGUI _leftChoiceText;
        [SerializeField] private TextMeshProUGUI _rightChoiceText;
        private Indicator[] _indicators;

        protected override void OnAwake() 
            => _indicators = _indicatorPanel.GetComponentsInChildren<Indicator>();

        public void UpdateCardView(DialogueCard newCard)
        {
            _characterPhrase.text = newCard.CharacterPhraseValue;
            _leftChoiceText.text = newCard.ChoiceElementsData[(int) ChoiceSide.Left].ChoiceTextValue;
            _rightChoiceText.text = newCard.ChoiceElementsData[(int) ChoiceSide.Right].ChoiceTextValue;
            _characterName.text = newCard.Character.Name;
            _portrait.sprite = newCard.Character.GetEmotionByType(newCard.EmotionValue).Sprite;
        }

        public void SetIndicatorValues(IndicatorData data)
        {
            List<int> values = data.ValuesList;
            for (int i = 0; i < _indicators.Length; i++)
            {
                _indicators[i].SetFillerValue(values[i]);
            }
        }
    }
}