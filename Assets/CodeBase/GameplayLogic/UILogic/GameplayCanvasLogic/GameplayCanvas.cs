using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayCanvas : UICanvas, IGameplayCanvas
    {
        [SerializeField] GameplayPanel _gameplayPanel;

        public void Initialize(IRuleManager ruleManager, ITurnManager turnManager)
        {
            base.Close();

            ruleManager.OnGameStarted += base.Open;
            
            _gameplayPanel.Initialize(turnManager);
        }
    }
}