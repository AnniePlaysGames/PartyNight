using CodeBase.Infrastructure.Services.Dialogues.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "DialogueSystem/Emotion")]
    public class Emotion : ScriptableObject
    {
        public EmotionType EmotionType;
        public Sprite Sprite;
    }
}