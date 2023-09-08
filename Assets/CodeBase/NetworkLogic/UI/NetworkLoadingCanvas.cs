using CodeBase.GameplayLogic.UILogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using CodeBase.NetworkLogic.RoomManagerLogic;
using UnityEngine;

namespace CodeBase.NetworkLogic.UI
{
    public class NetworkLoadingCanvas : UICanvas, INetworkLoadingCanvas
    {
        private INetworkManager _networkManager;
        private INetworkRoomManager _networkRoomManager;

        [SerializeField] NetworkLoadingPanel _loadingPanel;
        
        public void Initialize()
        {
            base.Close();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkRoomManager= ServiceLocator.Get<INetworkRoomManager>();
            
            _networkManager.Mediator.OnConnecting += base.Open;
            _networkManager.Mediator.OnConnected += base.Close;
            // _networkManager.Mediator.OnJoiningLobby += base.Open;
            // _networkManager.Mediator.OnJoinedLobby += base.Close;
            _networkRoomManager.Mediator.OnRoomCreatingFailed += base.Close;
            _networkRoomManager.Mediator.OnJoiningRoom += base.Open;
            _networkRoomManager.Mediator.OnJoinedRoom += base.Close;
            _networkRoomManager.Mediator.OnJoinRoomFailed += base.Close;
            _networkRoomManager.Mediator.OnRoomLeaving += base.Open;
            _networkRoomManager.Mediator.OnRoomLeaved += base.Close;
            
            _loadingPanel.Initialize();
        }
    }
}