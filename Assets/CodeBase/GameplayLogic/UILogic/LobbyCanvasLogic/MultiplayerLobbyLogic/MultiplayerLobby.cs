using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using CodeBase.NetworkLogic.LobbyLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using CodeBase.NetworkLogic.RoomManagerLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public class MultiplayerLobby : LobbyPanel
    {
        private INetworkManager _networkManager;
        private INetworkLobbyManager _networkLobbyManager;
        private INetworkRoomManager _networkRoomManager;
        
        [SerializeField] private CreateNewRoomPanel _createNewRoomPanel;
        [SerializeField] private MultiplayerRoomListManager _multiplayerRoomListManager;
        [SerializeField] private RectTransform _notConnectedNetworkOptions;
        [SerializeField] private RectTransform _connectedNetworkOptions;
        
        public override async Task Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            await base.Initialize(lobbyPanelsManager);

            _type = LobbyPanelType.MultiplayerLobby;
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkLobbyManager = ServiceLocator.Get<INetworkLobbyManager>();
            _networkRoomManager = ServiceLocator.Get<INetworkRoomManager>();
            
            _networkLobbyManager.Mediator.OnJoinedLobby += PrepareNetworkOptions;
            _networkRoomManager.Mediator.OnJoinedRoom += SuccessfullyRoomJoining;
            
            _createNewRoomPanel.Initialize(this);
            await _multiplayerRoomListManager.Initialize();
        }
        
        public override void Show()
        {
            base.Show();
            PrepareNetworkOptions();
        }
        
        public void ConnectButton()
        {
            _networkManager.ConnectToServer();
        }
        
        public void CreateRoom(string roomName)
        {
            _networkRoomManager.CreateRoom(roomName);
        }
        
        void SuccessfullyRoomJoining()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.TeamSelection);
        }
        
        void PrepareNetworkOptions()
        {
            if (_networkManager.IsConnected() && _networkLobbyManager.IsInLobby())
            {
                SetNotConnectedNetworkOptionsStatus(false);
                SetConnectedNetworkOptionsStatus(true);
            }
            else
            {
                SetNotConnectedNetworkOptionsStatus(true);
                SetConnectedNetworkOptionsStatus(false);
            }
        }

        private void SetConnectedNetworkOptionsStatus(bool status) => _connectedNetworkOptions.gameObject.SetActive(status);
        private void SetNotConnectedNetworkOptionsStatus(bool status) => _notConnectedNetworkOptions.gameObject.SetActive(status);
    }
}