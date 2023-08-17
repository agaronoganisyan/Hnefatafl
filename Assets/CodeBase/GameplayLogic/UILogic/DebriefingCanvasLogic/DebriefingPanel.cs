using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingPanel : MonoBehaviour
    {
        IGameManager _gameManager;

        [SerializeField] Image _panel;

        [SerializeField] Color _whitePanelColor;
        [SerializeField] Color _blackPanelColor;

        public void Initialize(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Open(TeamType type)
        {
            Prepare(type);
        }

        void Prepare(TeamType type)
        {
            if (type == TeamType.White) _panel.color = _whitePanelColor;
            else if (type == TeamType.Black) _panel.color = _blackPanelColor;
        }

        public void RestartButtonVoid()
        {
            _gameManager.RestartGame();
        }
    }
}