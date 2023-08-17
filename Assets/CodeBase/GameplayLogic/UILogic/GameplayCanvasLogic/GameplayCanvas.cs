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

        public void Initialize(IGameManager gameManager, ITurnManager turnManager)
        {
            base.Close();

            gameManager.OnGameStarted += base.Open;
            
            _gameplayPanel.Initialize(turnManager);
        }
    }
}