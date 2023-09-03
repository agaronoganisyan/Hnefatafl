using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.RoomLogic;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class PlaymodeSelectionPanel : LobbyPanel
    {
        private IGameRoomHandler _gameRoomHandler;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.PlaymodeSelection;

            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
        }

        public void JumpToMultiplayerPanel()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.MultiplayerLobby);
        }
        
        public void SingleplayerButton()
        {
            _lobbyPanelsManager.HideCanvas();
            
            _gameRoomHandler.TryToStartGame();
        }
    }
}