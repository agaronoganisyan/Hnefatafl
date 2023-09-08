using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using Photon.Pun;

namespace CodeBase.NetworkLogic.LobbyLogic
{
    public class NetworkLobbyManager : NetworkLobbyCallbacks, INetworkLobbyManager
    {
        public INetworkLobbyMediator Mediator => _mediator;
        private INetworkLobbyMediator _mediator;
        
        private INetworkManager _networkManager;
        
        public void Initialize()
        {
            _mediator = new NetworkLobbyMediator();
            
            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.AddCallbackTarget(this);
        }
        
        public bool IsInLobby()
        {
            return PhotonNetwork.InLobby;
        }
        
        public void JoinDefaultLobby()
        {
            if (!IsInLobby())
            {
                PhotonNetwork.JoinLobby(NetworkConstValues.DefaultLobby);
            
                _mediator.NotifyAboutJoiningLobby();
                _networkManager.ChangeConnectionStatus("Join Default Lobby");
            }
        }
        
        public override void OnJoinedLobby()
        {
            _mediator.NotifyAboutSuccessfulJoiningLobby();
            _networkManager.ChangeConnectionStatus("Joined Lobby");
        }
    }
}