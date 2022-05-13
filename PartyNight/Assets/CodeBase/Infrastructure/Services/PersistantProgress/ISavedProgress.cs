using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgress : ISavedProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}