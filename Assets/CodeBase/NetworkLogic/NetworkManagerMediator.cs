using System;

namespace CodeBase.NetworkLogic
{
    public class NetworkManagerMediator : INetworkManagerMediator
    {
        public event Action<string> OnConnectionStatusChanged;
        public event Action OnConnected;
        public event Action OnJoinedRoom;
        public event Action OnJoinRoomFailed;

        public void NotifyAboutChangingConnectionStatus(string status)
        {
            OnConnectionStatusChanged?.Invoke(status);
        }
        public void NotifyAboutJoiningRoom()
        {
            OnJoinedRoom?.Invoke();
        }

        public void NotifyAboutFailedJoiningRoom()
        {
            OnJoinRoomFailed?.Invoke();
        }

        public void NotifyAboutConnecting()
        {
            OnConnected?.Invoke();
        }
    }
}