using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using CodeBase.Infrastructure.Services.PersistantProgress;

namespace CodeBase.Infrastructure.Services.Stats
{
    public interface IStatsService : IService, ISavedProgress
    {
        public IndicatorData CurrentData { get; }
        void UpdateCurrentData(IndicatorUpdateData impact);
    }
}