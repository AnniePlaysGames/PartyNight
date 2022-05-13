using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using UnityEngine;

namespace CodeBase.DialogueSystem.Editor.Data.SaveLoad
{
    [Serializable]
    public class DSNodeSaveData
    {
        [field: SerializeField] public string DialogueNameValue { get; set; } = "DialogueName";
        [field: SerializeField] public List<ChoiceData> ChoiceData { get; set; } = new List<ChoiceData>();
        [field: SerializeField] public CharacterType CharacterValue { get; set; } = CharacterType.None;
        [field: SerializeField] public EmotionType EmotionValue { get; set; } = EmotionType.Default;
        [field: SerializeField] public string CharacterPhraseValue { get; set; } = "CharacterPhrase";
        [field: SerializeField] public Rect Position { get; set; } = new Rect(Vector2.zero, new Vector2(10, 10));
        [field: SerializeField] public string ID { get; set; } = Guid.NewGuid().ToString();

        public DialogueCard ConvertToDialogueCard() 
            => new DialogueCard(DialogueNameValue, ChoiceData.ToArray(), CharacterValue, EmotionValue, CharacterPhraseValue, ID);
    }
}