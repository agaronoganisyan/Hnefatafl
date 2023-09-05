using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class PlaymodeSelectionPanel : LobbyPanel
    {
        private IPlaymodeManager _playmodeManager;
        private IGameRoomHandler _gameRoomHandler;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.PlaymodeSelection;

            _playmodeManager = ServiceLocator.Get<IPlaymodeManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
        }

        public void MultiplayerButton()
        {
            _playmodeManager.SetPlaymodeType(PlaymodeType.Multiplayer);
            
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.MultiplayerLobby);
        }
        
        public void SingleplayerButton()
        {
            _playmodeManager.SetPlaymodeType(PlaymodeType.Singleplayer);
            
            _lobbyPanelsManager.HideCanvas();
            _gameRoomHandler.TryToStartGame();
        }
    }
}