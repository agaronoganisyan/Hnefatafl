using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.LobbyLogic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.NetworkLogic.ManagerLogic
{
    public class NetworkManager : NetworkCallbacks , INetworkManager
    {
        public INetworkManagerMediator Mediator => _mediator;
        private INetworkManagerMediator _mediator;

        private INetworkLobbyManager _networkLobbyManager;
        
        public void Initialize()
        {
            _mediator = new NetworkManagerMediator();

            _networkLobbyManager = ServiceLocator.Get<INetworkLobbyManager>();
            
            AddCallbackTarget(this);
        }

        public bool IsConnected()
        {
            return PhotonNetwork.IsConnected;
        }

        public void AddCallbackTarget(object target)
        {
            PhotonNetwork.AddCallbackTarget(target);
        }

        public void ChangeConnectionStatus(string status)
        {
            _mediator.NotifyAboutChangingConnectionStatus(status);
        }

        public void ConnectToServer()
        {
            if (IsConnected())
            {
                _networkLobbyManager.JoinDefaultLobby();
            }
            else
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();
            
                _mediator.NotifyAboutConnecting();
                ChangeConnectionStatus("Connect To Server");   
            }
        }
        
        public override void OnConnectedToMaster()
        {
            _mediator.NotifyAboutSuccessfulConnecting();
            ChangeConnectionStatus("Connected To Master");
            
            _networkLobbyManager.JoinDefaultLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            ChangeConnectionStatus("Disconnected");

            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        }
    }
}
