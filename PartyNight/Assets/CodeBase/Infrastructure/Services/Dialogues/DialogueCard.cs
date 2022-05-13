using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues
{
    [Serializable]
    public class DialogueCard
    { 
        private Character _character;
        [field: SerializeField] public ChoiceData.ChoiceData[] ChoiceElementsData { get; set; }
        [field: SerializeField] public string DialogueNameValue { get; set; }
        public Character Character
        {
            get => _character ??= LoadCharacterFromResources(path: CharacterAssetPath[CharacterValue]);
            private set => _character = value;
        }
        [field: SerializeField] public CharacterType CharacterValue { get; set; }
        [field: SerializeField] public EmotionType EmotionValue { get; set; }
        [field: SerializeField] public string CharacterPhraseValue { get; set; }
        [field: SerializeField] public string ID { get; set; }

        public DialogueCard(string dialogueNameValue, ChoiceData.ChoiceData[] choiceElementsData,
            CharacterType characterValue, EmotionType emotionValue,
            string characterPhraseValue, string id)
        {
            DialogueNameValue = dialogueNameValue;
            ChoiceElementsData = choiceElementsData;
            CharacterValue = characterValue;
            EmotionValue = emotionValue;
            CharacterPhraseValue = characterPhraseValue;
            ID = id;
        }

        private Character LoadCharacterFromResources(string path) 
            => Resources.Load<Character>(path);
        
        private static readonly Dictionary<CharacterType, string> CharacterAssetPath =
            new Dictionary<CharacterType, string>()
            {
                [CharacterType.None] = "DialogueSystem/Characters/None",
                [CharacterType.Karina] = "DialogueSystem/Characters/Karina",
                [CharacterType.Nikita] = "DialogueSystem/Characters/Nikita",
                [CharacterType.Artem] = "DialogueSystem/Characters/Artem"
            };
    }
}