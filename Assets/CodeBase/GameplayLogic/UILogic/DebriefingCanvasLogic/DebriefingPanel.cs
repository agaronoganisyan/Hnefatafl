using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingPanel : MonoBehaviour
    {
        GameManager _gameManager;

        [SerializeField] Image _panel;

        [SerializeField] Color _winPanelColor;
        [SerializeField] Color _defeatPanelColor;

        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Open(DebriefingType type)
        {
            Prepare(type);
        }

        void Prepare(DebriefingType type)
        {
            if (type == DebriefingType.Win) _panel.color = _winPanelColor;
            else if (type == DebriefingType.Defeat) _panel.color = _defeatPanelColor;
        }

        public void RestartButtonVoid()
        {
            _gameManager.RestartGame();
        }
    }
}