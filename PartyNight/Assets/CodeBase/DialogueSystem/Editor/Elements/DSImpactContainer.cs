using System;
using System.Linq;
using CodeBase.DialogueSystem.Editor.Utilities;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace CodeBase.DialogueSystem.Editor.Elements
{
    [Serializable]
    public class ImpactContainer : VisualElement
    {
        private const int IndicatorsCount = 4;
        private readonly IntegerField[] _indicators;

        public ImpactContainer(IndicatorUpdateData data)
        {
            _indicators = new IntegerField[IndicatorsCount];
            for (var i = 0; i < _indicators.Length; i++)
            {
                _indicators[i] = new IntegerField
                {
                    value = data.ValuesList[i]
                };
            }

            AddToClassList(Utilities.Constants.HorizontalGroupStyle);
            this.AddSeveral(_indicators);
        }

        public IndicatorUpdateData GetImpactData() 
            => new IndicatorUpdateData(_indicators.Select(field => field.value).ToList());
    }
}