using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects
{
    public class Dialogue : ScriptableObject
    {
        [field: SerializeField] public List<DialogueCard> DialogueCards { get; set; } = new List<DialogueCard>();
    }
}