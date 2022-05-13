using CodeBase.Infrastructure.Data;

namespace CodeBase.Infrastructure.Services.PersistantProgress
{
    public interface IPersistantProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}