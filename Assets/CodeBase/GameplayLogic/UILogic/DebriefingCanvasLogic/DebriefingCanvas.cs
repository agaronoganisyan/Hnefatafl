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

        public void Initialize(IRuleManager ruleManager)
        {
            base.Close();

            ruleManager.OnGameStarted += base.Close;
            ruleManager.OnWhiteTeamWon += OpenWhiteScreen;
            ruleManager.OnBlackTeamWon += OpenBlackScreen;
            
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