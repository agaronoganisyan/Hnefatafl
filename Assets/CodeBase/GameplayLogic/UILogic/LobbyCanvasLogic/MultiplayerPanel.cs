using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using TMPro;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class MultiplayerPanel : LobbyPanel
    {
        private INetworkManager _networkManager;
        
        [SerializeField] private TextMeshProUGUI _connectionStatusText;
        
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);

            _networkManager = ServiceLocator.Get<INetworkManager>();
            ServiceLocator.Get<INetworkManager>().NetworkManagerMediator.OnConnectionStatusChanged += SetConnectionStatus;

            _type = LobbyPanelType.MultiplayerPlaymode;
        }
        
        public void ConnectButton()
        {
            _networkManager.ConnectToServer();
        }
        
        void SetConnectionStatus(string status)
        {
            _connectionStatusText.text = status;
        }
    }
}