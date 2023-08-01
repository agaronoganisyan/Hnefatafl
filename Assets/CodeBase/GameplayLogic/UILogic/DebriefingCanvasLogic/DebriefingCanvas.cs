using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public enum DebriefingType
    {
        None,
        Win,
        Defeat
    }

    public class DebriefingCanvas : UICanvas
    {
        [SerializeField] DebriefingPanel _debriefingPanel;

        public void Initialize(GameManager gameManager)
        {
            base.Close();

            _debriefingPanel.Initialize(gameManager);

            GameManager.OnGameWon += OpenWinScreen;
            GameManager.OnGameDefeated += OpenDefeatScreen;
        }


        void OpenWinScreen()
        {
            base.Open();
            _debriefingPanel.Open(DebriefingType.Win);
        }

        void OpenDefeatScreen()
        {
            base.Open();
            _debriefingPanel.Open(DebriefingType.Defeat);
        }
    }
}