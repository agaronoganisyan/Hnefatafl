using System;

namespace CodeBase.NetworkLogic
{
    public class NetworkManagerMediator : INetworkManagerMediator
    {
        public event Action<string> OnConnectionStatusChanged;
        public event Action OnJoinedRoom;

        public void NotifyAboutSelectedUnit(string status)
        {
            OnConnectionStatusChanged?.Invoke(status);
        }
        public void NotifyAboutJoiningRoom()
        {
            OnJoinedRoom?.Invoke();
        }
    }
}