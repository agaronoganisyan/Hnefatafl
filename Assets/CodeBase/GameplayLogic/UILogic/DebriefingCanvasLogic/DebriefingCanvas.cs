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

        public void Initialize(IGameManager gameManager)
        {
            base.Close();

            gameManager.OnGameStarted += base.Close;
            gameManager.OnWhiteTeamWon += OpenWhiteScreen;
            gameManager.OnBlackTeamWon += OpenBlackScreen;
            
            _debriefingPanel.Initialize(gameManager);
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