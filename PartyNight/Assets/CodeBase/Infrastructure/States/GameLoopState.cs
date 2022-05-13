using CodeBase.Infrastructure.Services.Dialogues;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IDialogueService _dialogueService;

        public GameLoopState(GameStateMachine stateMachine, IDialogueService dialogueService)
        {
            _dialogueService = dialogueService;
        }

        public void Exit()
        {
        }

        public void Enter()
        {
            _dialogueService.InitDialogues();
        }
    }
}