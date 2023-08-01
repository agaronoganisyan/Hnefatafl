using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        
    }
}