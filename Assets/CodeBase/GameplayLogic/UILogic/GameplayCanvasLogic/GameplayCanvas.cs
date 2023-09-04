using UnityEngine;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.RoomLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayCanvas : UICanvas, IGameplayCanvas
    {
        [SerializeField] GameplayPanel _gameplayPanel;

        public void Initialize()
        {
            base.Close();

            ServiceLocator.Get<IGameRoomHandler>().GameRoomHandlerMediator.OnQuitRoom += base.Close;
            ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameStarted += base.Open;
            
            _gameplayPanel.Initialize();
        }
    }
}