using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayCanvas : UICanvas, IGameplayCanvas
    {
        [SerializeField] GameplayPanel _gameplayPanel;

        public void Initialize()
        {
            base.Close();

            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameStarted += base.Open;
            
            _gameplayPanel.Initialize();
        }
    }
}