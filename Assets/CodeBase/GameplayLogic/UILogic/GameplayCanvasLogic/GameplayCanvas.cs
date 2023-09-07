using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using UnityEngine;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayCanvas : UICanvas, IGameplayCanvas, IGameplayModeChangingObserver
    {
        private IGameRoomHandler _gameRoomHandler;
        private IRuleManager _ruleManager;
        
        [SerializeField] GameplayPanel _gameplayPanel;

        public void Initialize()
        {
            base.Close();

            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            
            _gameRoomHandler.Mediator.OnQuitRoom += base.Close;
            _ruleManager.RuleManagerMediator.OnGameStarted += base.Open;
            
            _gameplayPanel.Initialize();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }
        
        public void UpdateChangedProperties()
        {
            _gameRoomHandler.Mediator.OnQuitRoom -= base.Close;
            _ruleManager.RuleManagerMediator.OnGameStarted -= base.Open;
            
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            
            _gameRoomHandler.Mediator.OnQuitRoom += base.Close;
            _ruleManager.RuleManagerMediator.OnGameStarted += base.Open;
        }
    }
}