using System;

namespace CodeBase.NetworkLogic.ManagerLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnConnecting; 
        event Action OnConnected;

        void NotifyAboutChangingConnectionStatus(string status);
        void NotifyAboutConnecting();
        void NotifyAboutSuccessfulConnecting();
    }
}