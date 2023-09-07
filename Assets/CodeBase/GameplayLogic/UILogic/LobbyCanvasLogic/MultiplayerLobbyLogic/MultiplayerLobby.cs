using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerLobbyLogic
{
    public class MultiplayerLobby : LobbyPanel
    {
        private INetworkManager _networkManager;

        [SerializeField] private CreateNewRoomPanel _createNewRoomPanel;
        [SerializeField] private MultiplayerRoomListManager _multiplayerRoomListManager;
        [SerializeField] private RectTransform _notConnectedNetworkOptions;
        [SerializeField] private RectTransform _connectedNetworkOptions;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);

            _type = LobbyPanelType.MultiplayerLobby;
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.Mediator.OnJoinedLobby += PrepareNetworkOptions;
            _networkManager.Mediator.OnJoinedRoom += SuccessfullyRoomJoining;
            
            _createNewRoomPanel.Initialize(this);
            _multiplayerRoomListManager.Initialize();
        }
        
        public override void Show()
        {
            base.Show();
            PrepareNetworkOptions();
        }
        
        public void ConnectButton()
        {
            _networkManager.ConnectToServer();
            
            SetNotConnectedNetworkOptionsStatus(false);
        }
        
        public void CreateRoom(string roomName)
        {
            _networkManager.CreateRoom(roomName);
            
            SetConnectedNetworkOptionsStatus(false);
        }
        
        void SuccessfullyRoomJoining()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.TeamSelection);
        }
        
        void PrepareNetworkOptions()
        {
            if (_networkManager.IsConnected() && _networkManager.IsInLobby())
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