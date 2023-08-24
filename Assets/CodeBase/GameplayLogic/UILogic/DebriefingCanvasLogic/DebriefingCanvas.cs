using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : UICanvas,IDebriefingCanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize(IRuleManager ruleManager, IRuleManagerMediator ruleManagerMediator)
        {
            base.Close();

            ruleManagerMediator.OnGameStarted += base.Close;
            ruleManagerMediator.OnWhiteTeamWon += OpenWhiteScreen;
            ruleManagerMediator.OnBlackTeamWon += OpenBlackScreen;
            
            _debriefingPanel.Initialize(ruleManager);
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