using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services
{
    public class PlaymodeManager : IPlaymodeManager
    {
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
        
        public void Initialize()
        {
            _singleplayerRuleManager = new SingleplayerRuleManager();
            _singleplayerGameRoomHandler = new SingleplayerRoomHandler();
            _singleplayerUnitsCommander = new SingleplayerUnitsCommander();
            _singleplayerInputHandler = new SingleplayerInputHandler();
            
            _singleplayerRuleManager.Initialize();
            _singleplayerGameRoomHandler.Initialize();
            _singleplayerUnitsCommander.Initialize();
            _singleplayerInputHandler.Initialize();
            
            _multiplayerRuleManager = new MultiplayerRuleManager();
            _multiplayerGameRoomHandler = new MultiplayerRoomHandler();
            _multiplayerUnitsCommander = new MultiplayerUnitsCommander();
            _multiplayerInputHandler = new MultiplayerInputHandler();
            
            _multiplayerRuleManager.Initialize();
            _multiplayerGameRoomHandler.Initialize();
            _multiplayerUnitsCommander.Initialize();
            _multiplayerInputHandler.Initialize();
        }

        public void SetPlaymodeType(PlaymodeType playmodeType)
        {
            return;
            
            if (playmodeType == PlaymodeType.Singleplayer) SetSingleplayerMode();
            else if (playmodeType == PlaymodeType.Multiplayer) SetMultiplayerMode();
        }

        private void SetSingleplayerMode()
        {
            ServiceLocator.ReRegister<IRuleManager>(_singleplayerRuleManager);
            ServiceLocator.ReRegister<IGameRoomHandler>(_singleplayerGameRoomHandler);
            ServiceLocator.ReRegister<IUnitsCommander>(_singleplayerUnitsCommander);
            ServiceLocator.ReRegister<IInputHandler>(_singleplayerInputHandler);
        }

        private void SetMultiplayerMode()
        {
            ServiceLocator.ReRegister<IRuleManager>(_multiplayerRuleManager);
            ServiceLocator.ReRegister<IGameRoomHandler>(_multiplayerGameRoomHandler);
            ServiceLocator.ReRegister<IUnitsCommander>(_multiplayerUnitsCommander);
            ServiceLocator.ReRegister<IInputHandler>(_multiplayerInputHandler);
        }
    }
}