using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : UICanvas, IDebriefingCanvas, IGameplayModeChangingObserver
    {
        private IGameRoomHandler _gameRoomHandler;
        private IRuleManager _ruleManager;
        
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize()
        {
            base.Close();

            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            _ruleManager= ServiceLocator.Get<IRuleManager>();
            
            _gameRoomHandler.Mediator.OnQuitRoom += base.Close;
            _ruleManager.RuleManagerMediator.OnWhiteTeamWon += OpenWhiteScreen;
            _ruleManager.RuleManagerMediator.OnBlackTeamWon += OpenBlackScreen;
            
            _debriefingPanel.Initialize();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
            _gameRoomHandler.Mediator.OnQuitRoom -= base.Close;
            _ruleManager.RuleManagerMediator.OnWhiteTeamWon -= OpenWhiteScreen;
            _ruleManager.RuleManagerMediator.OnBlackTeamWon -= OpenBlackScreen;
            
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            _ruleManager= ServiceLocator.Get<IRuleManager>();
            
            _gameRoomHandler.Mediator.OnQuitRoom += base.Close;
            _ruleManager.RuleManagerMediator.OnWhiteTeamWon += OpenWhiteScreen;
            _ruleManager.RuleManagerMediator.OnBlackTeamWon += OpenBlackScreen;
        }

        void OpenWhiteScreen()
        {
            base.Open();
            _debriefingPanel.Open(TeamType.White);
        }

        void OpenBlackScreen()
        {
            base.Open();
            _debriefingPanel.Open(TeamType.Black);
        }
    }
}