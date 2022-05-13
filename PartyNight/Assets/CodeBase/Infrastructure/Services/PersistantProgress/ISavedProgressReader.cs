using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}