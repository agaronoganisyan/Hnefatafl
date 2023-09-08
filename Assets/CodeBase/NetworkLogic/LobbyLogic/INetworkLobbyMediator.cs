using System;

namespace CodeBase.NetworkLogic.LobbyLogic
{
    public interface INetworkLobbyMediator
    {
        event Action OnJoiningLobby;
        event Action OnJoinedLobby;
        
        void NotifyAboutJoiningLobby();
        void NotifyAboutSuccessfulJoiningLobby();
    }
}