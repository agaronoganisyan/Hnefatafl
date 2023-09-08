using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.RoomManagerLogic
{
    public interface INetworkRoomMediator
    {
        event Action OnRoomCreatingFailed;
        event Action OnJoiningRoom;
        event Action OnJoinedRoom;
        event Action OnJoinRoomFailed;
        event Action OnRoomLeaving;
        event Action OnRoomLeaved;
        event Action OnOpponentLeavedRoom;
        event Action<List<RoomInfo>> OnRoomListUpdated;
        
        void NotifyAboutJoiningRoom();
        void NotifyAboutSuccessfulJoiningRoom();
        void NotifyAboutFailedJoiningRoom();
        void NotifyAboutRoomListUpdating(List<RoomInfo> roomInfos);
        void NotifyAboutLeavingRoom();
        void NotifyAboutSuccessfulLeavingRoom();
        void NotifyAboutOpponentLeaving();
        void NotifyAboutFailedRoomCreating();
    }
}