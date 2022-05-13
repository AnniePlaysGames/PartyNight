using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Dialogues;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.Services.Stats;
using CodeBase.UI;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, ServiceLocator serviceLocator,
            RectTransform uiRoot)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator, uiRoot),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, serviceLocator.Single<IWindowService>(), serviceLocator.Single<IStatsService>(), serviceLocator.Single<IPersistantProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, serviceLocator.Single<IPersistantProgressService>(), serviceLocator.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, serviceLocator.Single<IDialogueService>())
            };
        }
        
        public void Enter<TState>() where TState : class, IState 
            => ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> 
            => ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            
            TState state = GetState<TState>();
            _activeState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState 
            => _states[typeof(TState)] as TState;
    }
}