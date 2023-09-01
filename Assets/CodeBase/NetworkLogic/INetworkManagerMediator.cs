using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnConnected;
        event Action OnJoinedRoom;
        event Action OnJoinRoomFailed;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        void NotifyAboutChangingConnectionStatus(string status);
        void NotifyAboutJoiningRoom();
        void NotifyAboutFailedJoiningRoom();
        void NotifyAboutConnecting();
        void NotifyAboutRoomListUpdating(List<RoomInfo> roomInfos);

    }
}