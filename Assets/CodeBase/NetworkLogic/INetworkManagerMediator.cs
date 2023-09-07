using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnConnecting; 
        event Action OnConnected;
        event Action OnJoiningLobby;
        event Action OnJoinedLobby;
        event Action OnJoiningRoom;
        event Action OnJoinedRoom;
        event Action OnJoinRoomFailed;
        event Action OnRoomLeaving;
        event Action OnRoomLeaved;
        event Action OnOpponentLeavedRoom;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        void NotifyAboutChangingConnectionStatus(string status);
        void NotifyAboutConnecting();
        void NotifyAboutSuccessfulConnecting();
        void NotifyAboutJoiningLobby();
        void NotifyAboutSuccessfulJoiningLobby();
        void NotifyAboutJoiningRoom();
        void NotifyAboutSuccessfulJoiningRoom();
        void NotifyAboutFailedJoiningRoom();
        void NotifyAboutRoomListUpdating(List<RoomInfo> roomInfos);
        void NotifyAboutLeavingRoom();
        void NotifyAboutSuccessfulLeavingRoom();
        void NotifyAboutOpponentLeaving();
    }
}