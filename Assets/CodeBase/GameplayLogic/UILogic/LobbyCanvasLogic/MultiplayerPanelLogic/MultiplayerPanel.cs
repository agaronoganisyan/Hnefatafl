using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic.MultiplayerPanelLogic
{
    public class MultiplayerPanel : LobbyPanel
    {
        private INetworkManager _networkManager;

        [SerializeField] private CreateNewRoomPanel _createNewRoomPanel;
        
        [SerializeField] private RectTransform _notConnectedNetworkOptions;
        [SerializeField] private RectTransform _connectedNetworkOptions;

        [SerializeField] private TextMeshProUGUI _connectionStatusText;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);

            _type = LobbyPanelType.MultiplayerPlaymode;
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnConnectionStatusChanged += SetConnectionStatus;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnConnected += PrepareNetworkOptions;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoinedRoom += SuccessfullyRoomJoining;
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnJoinRoomFailed += FailedRoomJoining;
            
            _createNewRoomPanel.Initialize(this);
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

        public void JoinRandomRoomButton()
        {
            _networkManager.JoinRandomRoom();
            
            _connectedNetworkOptions.gameObject.SetActive(false);
        }
        
        void SuccessfullyRoomJoining()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.TeamSelection);
        }

        void FailedRoomJoining()
        {
            _connectedNetworkOptions.gameObject.SetActive(true);
        }
        
        void SetConnectionStatus(string status)
        {
            _connectionStatusText.text = status;
        }
        
        void PrepareNetworkOptions()
        {
            if (_networkManager.IsConnected())
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
    }
}