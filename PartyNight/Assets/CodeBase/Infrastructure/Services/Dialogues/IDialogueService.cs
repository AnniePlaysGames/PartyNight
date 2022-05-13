using CodeBase.Infrastructure.Services.Dialogues.Enums;

namespace CodeBase.Infrastructure.Services.Dialogues
{
    public interface IDialogueService : IService
    { 
        void InitDialogues();
        void OnMoveToNextCard(ChoiceSide side);
    }
}