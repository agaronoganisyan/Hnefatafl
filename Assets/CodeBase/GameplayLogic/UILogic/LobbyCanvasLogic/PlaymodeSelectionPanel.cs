using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class PlaymodeSelectionPanel : LobbyPanel
    {
        private IGameplayModeManager _gameplayModeManager;
        private IGameRoomHandler _gameRoomHandler;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.PlaymodeSelection;

            _gameplayModeManager = ServiceLocator.Get<IGameplayModeManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
        }

        public void MultiplayerButton()
        {
            _gameplayModeManager.SetPlaymodeType(PlaymodeType.Multiplayer);
            
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.MultiplayerLobby);
        }
        
        public void SingleplayerButton()
        {
            _gameplayModeManager.SetPlaymodeType(PlaymodeType.Singleplayer);
            
            _lobbyPanelsManager.HideCanvas();
            _gameRoomHandler.TryToStartGame();
        }
    }
}