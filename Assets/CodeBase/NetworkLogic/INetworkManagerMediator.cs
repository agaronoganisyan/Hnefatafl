using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnConnected;
        event Action OnJoinedLobby;
        event Action OnJoiningRoom;
        event Action OnJoinedRoom;
        event Action OnJoinRoomFailed;
        event Action OnLeftRoom;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        void NotifyAboutChangingConnectionStatus(string status);
        void NotifyAboutConnecting();
        void NotifyAboutJoiningLobby();
        void NotifyAboutJoiningRoom();
        void NotifyAboutSuccessfulJoiningRoom();
        void NotifyAboutFailedJoiningRoom();
        void NotifyAboutRoomListUpdating(List<RoomInfo> roomInfos);
        void NotifyAboutLeavingRoom();
    }
}