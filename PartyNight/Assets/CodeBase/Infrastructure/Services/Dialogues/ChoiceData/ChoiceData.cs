using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Dialogues.ChoiceData
{
    [Serializable]
    public class ChoiceData
    {
        [field: SerializeField] public string ChoiceTextValue { get; set; } = "ChoiceText";
        [field: SerializeField] public IndicatorUpdateData Impact { get; set; } = new IndicatorUpdateData();
        [field: SerializeField] public string ConnectedNodeId { get; set; }
    }
}