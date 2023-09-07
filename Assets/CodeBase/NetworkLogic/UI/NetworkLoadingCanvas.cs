using CodeBase.GameplayLogic.UILogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.NetworkLogic.UI
{
    public class NetworkLoadingCanvas : UICanvas, INetworkLoadingCanvas
    {
        private INetworkManager _networkManager;
        
        [SerializeField] NetworkLoadingPanel _loadingPanel;
        
        public void Initialize()
        {
            base.Close();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.Mediator.OnConnecting += () =>
            {
                base.Open();
                Debug.Log("OnConnecting");
            };
            _networkManager.Mediator.OnConnected += base.Close;
            _networkManager.Mediator.OnJoiningLobby += () =>
            {
                base.Open();
                Debug.Log("OnJoiningLobby");
            };
            _networkManager.Mediator.OnJoinedLobby += base.Close;
            _networkManager.Mediator.OnJoiningRoom += () =>
            {
                base.Open();
                Debug.Log("OnJoiningRoom");
            };
            _networkManager.Mediator.OnJoinedRoom += base.Close;
            _networkManager.Mediator.OnJoinRoomFailed += base.Close;
            _networkManager.Mediator.OnRoomLeaving += () =>
            {
                base.Open();
                Debug.Log("OnRoomLeaving");
            };
            _networkManager.Mediator.OnRoomLeaved += base.Close;
            
            _loadingPanel.Initialize();
        }
    }
}