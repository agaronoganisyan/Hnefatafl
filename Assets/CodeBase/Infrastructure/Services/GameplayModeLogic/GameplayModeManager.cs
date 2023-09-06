using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.GameplayModeLogic
{
    public class GameplayModeManager : IGameplayModeManager
    {
        public GameplayModeManagerMediator Mediator => _mediator;
        private GameplayModeManagerMediator _mediator;
        
        //SingleplayerServices
        private IRuleManager _singleplayerRuleManager;
        private IGameRoomHandler _singleplayerGameRoomHandler;
        private IUnitsCommander _singleplayerUnitsCommander;
        private IInputHandler _singleplayerInputHandler;
        
        //MultiplayerServices
        private IRuleManager _multiplayerRuleManager;
        private IGameRoomHandler _multiplayerGameRoomHandler;
        private IUnitsCommander _multiplayerUnitsCommander;
        private IInputHandler _multiplayerInputHandler;

        private PlaymodeType _currentPlaymodeType;

        private bool _isSingleplayerServicesInitialized;
        private bool _isMultiplayerServicesInitialized;

        public void Initialize()
        {
            _mediator = new GameplayModeManagerMediator();
            
            _singleplayerRuleManager = new SingleplayerRuleManager();
            _singleplayerGameRoomHandler = new SingleplayerRoomHandler();
            _singleplayerUnitsCommander = new SingleplayerUnitsCommander();
            _singleplayerInputHandler = new SingleplayerInputHandler();
            
            _multiplayerRuleManager = new MultiplayerRuleManager();
            _multiplayerGameRoomHandler = new MultiplayerRoomHandler();
            _multiplayerUnitsCommander = new MultiplayerUnitsCommander();
            _multiplayerInputHandler = new MultiplayerInputHandler();
        }

        public void SetPlaymodeType(PlaymodeType playmodeType)
        {
            if (_currentPlaymodeType == playmodeType) return;
            
            if (playmodeType == PlaymodeType.Singleplayer) SetSingleplayerMode();
            else if (playmodeType == PlaymodeType.Multiplayer) SetMultiplayerMode();
        }

        private void SetSingleplayerMode()
        {
            _currentPlaymodeType = PlaymodeType.Singleplayer;
            
            ServiceLocator.ReRegister<IRuleManager>(_singleplayerRuleManager);
            ServiceLocator.ReRegister<IGameRoomHandler>(_singleplayerGameRoomHandler);
            ServiceLocator.ReRegister<IUnitsCommander>(_singleplayerUnitsCommander);
            ServiceLocator.ReRegister<IInputHandler>(_singleplayerInputHandler);
            
            InitializeReRegisteredServices(ref _isSingleplayerServicesInitialized);
            
            _mediator.NotifyAboutGameplayModeChanging();
        }

        private void SetMultiplayerMode()
        {
            _currentPlaymodeType = PlaymodeType.Multiplayer;
            
            ServiceLocator.ReRegister<IRuleManager>(_multiplayerRuleManager);
            ServiceLocator.ReRegister<IGameRoomHandler>(_multiplayerGameRoomHandler);
            ServiceLocator.ReRegister<IUnitsCommander>(_multiplayerUnitsCommander);
            ServiceLocator.ReRegister<IInputHandler>(_multiplayerInputHandler);
            
            InitializeReRegisteredServices(ref _isMultiplayerServicesInitialized);
            
            _mediator.NotifyAboutGameplayModeChanging();
        }

        private void InitializeReRegisteredServices(ref bool isInitialized)
        {
            if (isInitialized) return;
            isInitialized = true;
            
            Debug.Log("InitializeReRegisteredServices");
            
            ServiceLocator.Get<IRuleManager>().Initialize();
            ServiceLocator.Get<IGameRoomHandler>().Initialize();
            ServiceLocator.Get<IUnitsCommander>().Initialize();
            ServiceLocator.Get<IInputHandler>().Initialize();
        }
    }
}