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
        IRuleManager _ruleManager;

        [SerializeField] Image _panel;

        [SerializeField] Color _whitePanelColor;
        [SerializeField] Color _blackPanelColor;

        public void Initialize(IRuleManager ruleManager)
        {
            _ruleManager = ruleManager;
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
            _ruleManager.RestartGame();
        }
    }
}