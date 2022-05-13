using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public class PersistantProgressService : IPersistantProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}