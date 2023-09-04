using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
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
        [SerializeField] private RectTransform _backButton;

        [SerializeField] private TextMeshProUGUI _connectionStatusText;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);

            _type = LobbyPanelType.MultiplayerLobby;
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.NetworkManagerMediator.OnConnectionStatusChanged += SetConnectionStatus;
            _networkManager.NetworkManagerMediator.OnJoinedLobby += PrepareNetworkOptions;
            _networkManager.NetworkManagerMediator.OnJoiningRoom +=  TryJoinRoom;
            _networkManager.NetworkManagerMediator.OnJoinedRoom += SuccessfullyRoomJoining;
            _networkManager.NetworkManagerMediator.OnJoinRoomFailed += JoiningRoomFailed;
            
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
            SetBackButtonStatus(false);
        }
        
        public void CreateRoom(string roomName)
        {
            _networkManager.CreateRoom(roomName);
            
            SetConnectedNetworkOptionsStatus(false);
            SetBackButtonStatus(false);
        }
        
        void SuccessfullyRoomJoining()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.TeamSelection);
        }
        
        void SetConnectionStatus(string status)
        {
            _connectionStatusText.text = status;
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

            SetBackButtonStatus(true);
        }

        void TryJoinRoom()
        {
            SetConnectedNetworkOptionsStatus(false);
            SetBackButtonStatus(false);
        }

        void JoiningRoomFailed()
        {
             SetConnectedNetworkOptionsStatus(true);
             SetBackButtonStatus(true);
        }

        private void SetBackButtonStatus(bool status) => _backButton.gameObject.SetActive(status);
        private void SetConnectedNetworkOptionsStatus(bool status) => _connectedNetworkOptions.gameObject.SetActive(status);
        private void SetNotConnectedNetworkOptionsStatus(bool status) => _notConnectedNetworkOptions.gameObject.SetActive(status);
    }
}