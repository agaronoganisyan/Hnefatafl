using System.Threading.Tasks;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.EventsManagerLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using CodeBase.NetworkLogic.RoomManagerLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public class TeamSelectionPanel : LobbyPanel , IOnEventCallback, IGameplayModeChangingObserver
    {
        private INetworkManager _networkManager;
        private INetworkEventsManager _networkEventsManager;
        private INetworkRoomManager _networkRoomManager;
        private IGameRoomHandler _gameRoomHandler;

        [SerializeField] private Button _whiteTeamButton;
        [SerializeField] private Button _blackTeamButton;

        public override async Task Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            await base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.TeamSelection;

            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkEventsManager = ServiceLocator.Get<INetworkEventsManager>();
            _networkRoomManager= ServiceLocator.Get<INetworkRoomManager>();
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();

            _networkManager.AddCallbackTarget(this);
            
            _whiteTeamButton.onClick.RemoveAllListeners();
            _blackTeamButton.onClick.RemoveAllListeners();
            
            _whiteTeamButton.onClick.AddListener(() => SelectTeamButton(TeamType.White));
            _blackTeamButton.onClick.AddListener(() => SelectTeamButton(TeamType.Black));
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
            _gameRoomHandler = ServiceLocator.Get<IGameRoomHandler>();
        }
        
        public override void Show()
        {
            base.Show();
            PrepareTeamSelectionOptions();
        }

        void PrepareTeamSelectionOptions()
        {
            _whiteTeamButton.interactable = true;
            _blackTeamButton.interactable = true;
            
            TeamType selectedTeam = _networkRoomManager.GetSelectedTeamInRoom(_networkRoomManager.GetCurrentRoom());
            
            if (selectedTeam == TeamType.None) return;

            if (selectedTeam == TeamType.White)
            {
                _whiteTeamButton.interactable = false;
                _blackTeamButton.interactable = true;
            }
            else if (selectedTeam == TeamType.Black)
            {
                _whiteTeamButton.interactable = true;
                _blackTeamButton.interactable = false;
            }
        }

        void SelectTeamButton(TeamType selectedTeamType)
        {
            _networkRoomManager.SelectTeamInRoom(selectedTeamType,_networkRoomManager.GetCurrentRoom());

            _lobbyPanelsManager.HideCanvas();
            _gameRoomHandler.TryToStartGame();
            
            PrepareButtons(selectedTeamType);
            
            _networkEventsManager.RaiseSelectTeamEvent(selectedTeamType);
        }

        void PrepareButtons(TeamType selectedTeamType)
        {
            if (selectedTeamType == TeamType.White) _whiteTeamButton.interactable = false;
            else if (selectedTeamType == TeamType.Black) _blackTeamButton.interactable = false;
        }

        public void OnEvent(EventData photonEvent)
        {
            NetworkEventType type = _networkEventsManager.GetNetworkEventType(photonEvent);

            if (type == NetworkEventType.SelectTeam) PrepareButtons(_networkEventsManager.GetSelectTeamEventValue(photonEvent));
        }
    }
}