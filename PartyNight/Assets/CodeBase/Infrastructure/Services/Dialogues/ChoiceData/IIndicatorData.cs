using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services.Dialogues.ChoiceData
{
    public interface IIndicatorData
    {
        public List<int> ValuesList { get; }
        int Housekeeping { get; }
        int Fun { get; }
        int Drunkenness { get; }
        int Loudness { get; }
    }
}