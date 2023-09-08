using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.ManagerLogic
{
    public class NetworkManagerMediator : INetworkManagerMediator
    {
        public event Action<string> OnConnectionStatusChanged;
        public event Action OnConnecting;
        public event Action OnConnected;

        public void NotifyAboutChangingConnectionStatus(string status)
        {
            OnConnectionStatusChanged?.Invoke(status);
        }

        public void NotifyAboutConnecting()
        {
            OnConnecting?.Invoke();
        }

        public void NotifyAboutSuccessfulConnecting()
        {
            OnConnected?.Invoke();
        }
    }
}