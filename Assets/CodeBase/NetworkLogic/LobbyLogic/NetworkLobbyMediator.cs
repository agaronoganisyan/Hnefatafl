using System;

namespace CodeBase.NetworkLogic.LobbyLogic
{
    public class NetworkLobbyMediator : INetworkLobbyMediator
    {
        public event Action OnJoiningLobby;
        public event Action OnJoinedLobby;
        
        public void NotifyAboutJoiningLobby()
        {
            OnJoiningLobby?.Invoke();
        }

        public void NotifyAboutSuccessfulJoiningLobby()
        {
            OnJoinedLobby?.Invoke();
        }
    }
}