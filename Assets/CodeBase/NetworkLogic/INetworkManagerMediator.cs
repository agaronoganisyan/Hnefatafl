using System;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnConnected;
        event Action OnJoinedRoom;
        event Action OnJoinRoomFailed;
        void NotifyAboutChangingConnectionStatus(string status);
        void NotifyAboutJoiningRoom();
        void NotifyAboutFailedJoiningRoom();
        void NotifyAboutConnecting();
    }
}