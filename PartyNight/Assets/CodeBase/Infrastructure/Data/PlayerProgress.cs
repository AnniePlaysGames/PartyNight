using System;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;

namespace CodeBase.Infrastructure.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public IndicatorData indicatorData;
        public int dialogueIndex;
        public int cardIndex;
        
        public PlayerProgress()
        {
            indicatorData = new IndicatorData(5, 5, 5, 5);
            dialogueIndex = 0;
            cardIndex = 0;
        }
    }
}