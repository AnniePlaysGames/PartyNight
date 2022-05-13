using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Services.Dialogues.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "DialogueSystem/Character")]
    public class Character : ScriptableObject
    {
        public string Name;
        public Emotion[] Emotions;
        private Dictionary<EmotionType, Emotion> _emotionByType;

        public Emotion GetEmotionByType(EmotionType type)
        {
            _emotionByType ??= GenerateDictionary();
            return _emotionByType[type];
        }
        
        private Dictionary<EmotionType, Emotion> GenerateDictionary() 
            => Emotions.ToDictionary(emotion => emotion.EmotionType);
    }
}