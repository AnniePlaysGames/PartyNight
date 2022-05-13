using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.Stats;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistantProgressService _progress;
        private readonly IStatsService _statsService;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistantProgressService progress, IStatsService statsService)
        {
            _progress = progress;
            _statsService = statsService;
        }

        public void SaveProgress()
        {
            _statsService.UpdateProgress(_progress.PlayerProgress);
            PlayerPrefs.SetString(ProgressKey, _progress.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() 
            => PlayerPrefs.GetString(ProgressKey)?
                .ToDeserealized<PlayerProgress>();
    }
}