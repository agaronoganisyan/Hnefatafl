using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class PlaymodeSelectionPanel : LobbyPanel, IGameplayModeChangingObserver
    {
        private IGameplayModeManager _gameplayModeManager;
        private IGameRoomHandler _gameRoomHandler;
        
        public override async Task Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            await base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.PlaymodeSelection;

            _gameplayModeManager = ServiceLocator.Get<IGameplayModeManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
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