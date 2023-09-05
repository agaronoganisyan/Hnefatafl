using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class LobbyCanvas : UICanvas, ILobbyCanvas
    {
        [SerializeField] LobbyPanelsManager _lobbyPanelsManager;
        
        public void Initialize()
        {
            ServiceLocator.Get<IGameRoomHandler>().GameRoomHandlerMediator.OnQuitRoom += base.Open;
            
            _lobbyPanelsManager.Initialize(this);
        }

        public void ClosePanel()
        {
            base.Close();
        }
    }
}