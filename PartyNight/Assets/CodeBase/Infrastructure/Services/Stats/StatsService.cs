using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services.Dialogues.ChoiceData;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Stats
{
    public class StatsService : IStatsService
    {
        public IndicatorData CurrentData { get; private set; }

        public void LoadProgress(PlayerProgress progress)
        {
            CurrentData = progress.indicatorData;
            CurrentData.ValueIsOutOfRange += OnLose;
        }

        public void UpdateProgress(PlayerProgress progress) 
            => progress.indicatorData = CurrentData;

        public void UpdateCurrentData(IndicatorUpdateData impact) 
            => CurrentData.UpdateData(impact);

        private void OnLose(OutOfRangeType obj)
        {
            Debug.Log("Lose by" + obj);
            CurrentData.ValueIsOutOfRange -= OnLose;
        }
    }
}