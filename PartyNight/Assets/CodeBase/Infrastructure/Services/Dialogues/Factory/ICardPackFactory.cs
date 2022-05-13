using System.Collections.Generic;
using CodeBase.Infrastructure.Services.Dialogues.Scriptable_Objects;

namespace CodeBase.Infrastructure.Services.Dialogues.Factory
{
    public interface ICardPackFactory
    {
        List<Dialogue> GeneratePack();
    }
}