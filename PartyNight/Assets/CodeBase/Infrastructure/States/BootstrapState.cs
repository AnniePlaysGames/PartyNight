using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.InputService;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Stats;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _services;
        private readonly RectTransform _uiRoot;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator, RectTransform uiRoot)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = serviceLocator;
            _uiRoot = uiRoot;

            RegisterServices();
        }

        public void Enter() 
            => _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadProgressLevel);

        public void Exit()  
        {
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<IInputService>(new PCInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(), _uiRoot));
            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
            _services.RegisterSingle<IStatsService>(new StatsService());
            _services.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistantProgressService>(),_services.Single<IStatsService>()));
            _services.RegisterSingle<IDialogueService>(new DialogueService(_services.Single<IUIFactory>(), _services.Single<IStatsService>(), _services.Single<ISaveLoadService>(), _services.Single<IPersistantProgressService>()));
        }

        private void EnterLoadProgressLevel() 
            => _stateMachine.Enter<LoadProgressState>();
    }
}