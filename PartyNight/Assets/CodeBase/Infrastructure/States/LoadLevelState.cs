using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.Stats;
using CodeBase.UI;
using CodeBase.UI.Services.Windows;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IWindowService _windowService;
        private readonly IStatsService _statsService;
        private readonly IPersistantProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IWindowService windowService, IStatsService statsService, IPersistantProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _windowService = windowService;
            _statsService = statsService;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            InformProgressReaders();
            _windowService.Open(WindowId.Dialogue);
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            _statsService.LoadProgress(_progressService.PlayerProgress);
        }
        
        public void Exit() 
            => _curtain.Hide();
    }
}