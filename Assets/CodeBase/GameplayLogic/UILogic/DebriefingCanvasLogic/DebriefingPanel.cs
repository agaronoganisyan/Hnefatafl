using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Infrastructure;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic
{
    public class DebriefingPanel : MonoBehaviour
    {
        IRuleManager _ruleManager;

        [SerializeField] Image _panel;

        [SerializeField] Color _whitePanelColor;
        [SerializeField] Color _blackPanelColor;

        public void Initialize()
        {
            _ruleManager = ServiceLocator.Get<IRuleManager>();
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