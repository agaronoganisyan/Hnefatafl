using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingCanvas : UICanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize(GameManager gameManager)
        {
            base.Close();

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

        private void OnEnable()
        {
            GameManager.OnGameStarted += base.Close;
            GameManager.OnWhiteTeamWon += OpenWhiteScreen;
            GameManager.OnBlackTeamWon += OpenBlackScreen;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= base.Close;
            GameManager.OnWhiteTeamWon -= OpenWhiteScreen;
            GameManager.OnBlackTeamWon -= OpenBlackScreen;
        }
    }
}