using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayCanvas : UICanvas
    {
        [SerializeField] GameplayPanel _gameplayPanel;

        public void Initialize()
        {
            base.Close();

            _gameplayPanel.Initialize();
        }

        private void OnEnable()
        {
            GameManager.OnGameStarted += base.Open;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= base.Open;
        }
    }
}