using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : UICanvas,IDebriefingCanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize()
        {
            base.Close();

            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameStarted += base.Close;
            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnWhiteTeamWon += OpenWhiteScreen;
            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnBlackTeamWon += OpenBlackScreen;
            
            _debriefingPanel.Initialize();
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