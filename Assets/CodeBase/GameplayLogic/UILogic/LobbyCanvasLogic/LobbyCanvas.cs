using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class LobbyCanvas : UICanvas, ILobbyCanvas, IGameplayModeChangingObserver
    {
        private IGameRoomHandler _gameRoomHandler;
        
        [SerializeField] LobbyPanelsManager _lobbyPanelsManager;
        
        public async Task Initialize()
        {
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            _gameRoomHandler.Mediator.OnQuitRoom += base.Open;
            
            await _lobbyPanelsManager.Initialize(this);
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
            _gameRoomHandler.Mediator.OnQuitRoom -= base.Open;
            
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            
            _gameRoomHandler.Mediator.OnQuitRoom += base.Open;
        }
        
        public void ClosePanel()
        {
            base.Close();
        }
    }
}