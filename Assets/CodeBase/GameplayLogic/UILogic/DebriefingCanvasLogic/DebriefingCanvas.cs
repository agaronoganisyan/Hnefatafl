using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : UICanvas,IDebriefingCanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize()
        {
            base.Close();

            ServiceLocator.Get<IRuleManagerMediator>().OnGameStarted += base.Close;
            ServiceLocator.Get<IRuleManagerMediator>().OnWhiteTeamWon += OpenWhiteScreen;
            ServiceLocator.Get<IRuleManagerMediator>().OnBlackTeamWon += OpenBlackScreen;
            
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