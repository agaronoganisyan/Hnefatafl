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

        [SerializeField] private TextMeshProUGUI _connectionStatusText;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);

            _type = LobbyPanelType.MultiplayerLobby;
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnConnectionStatusChanged += SetConnectionStatus;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoinedLobby += PrepareNetworkOptions;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoiningRoom += HideConnectedNetworkOptions;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoinedRoom += SuccessfullyRoomJoining;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoinRoomFailed += ShowConnectedNetworkOptions;
            
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
            
            _notConnectedNetworkOptions.gameObject.SetActive(false);
        }
        
        public void CreateRoom(string roomName)
        {
            _networkManager.CreateRoom(roomName);
            
            _connectedNetworkOptions.gameObject.SetActive(false);
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
                _notConnectedNetworkOptions.gameObject.SetActive(false);
                _connectedNetworkOptions.gameObject.SetActive(true);
            }
            else
            {
                _notConnectedNetworkOptions.gameObject.SetActive(true);
                _connectedNetworkOptions.gameObject.SetActive(false);
            }
        }

        void HideConnectedNetworkOptions()
        {
            _connectedNetworkOptions.gameObject.SetActive(false);
        }
        
        void ShowConnectedNetworkOptions()
        {
            _connectedNetworkOptions.gameObject.SetActive(true);
        }
    }
}