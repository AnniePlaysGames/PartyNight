using System;
using System.Collections.Generic;
using CodeBase.DialogueSystem.Editor.Data.SaveLoad;
using CodeBase.DialogueSystem.Editor.Utilities;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Elements
{
    [Serializable]
    public class DSNode : Node
    {
        public DSNodeSaveData Data { get; set; }
        public TextField DialogueName { get; set; }
        public EnumField CharacterDropdown { get; set; }
        public EnumField EmotionDropdown { get; set; } 
        public TextField CharacterPhrase { get; set; } 
        public List<ChoiceElement> ChoiceElements { get; set; }
        public Rect Position { get; set; }
        public Port[] OutputPorts { get; set; } = new Port[Utilities.Constants.DefaultChoiceCount];
        public Port InputPort { get; set; }
        public string ID { get; set; }

        public DSNodeSaveData CollectNodeData()
        {
            DSNodeSaveData data = new DSNodeSaveData()
            {
                DialogueNameValue = DialogueName.value,
                CharacterValue = (CharacterType) CharacterDropdown.value,
                EmotionValue = (EmotionType) EmotionDropdown.value,
                CharacterPhraseValue = CharacterPhrase.value,
                ChoiceData = new List<ChoiceData>(),
                Position = GetPosition(),
                ID = ID
            };
            foreach (ChoiceElement element in ChoiceElements)
            {
                data.ChoiceData.Add(element.GetChoiceElementData());
            }
            return data;
        }
    }
}