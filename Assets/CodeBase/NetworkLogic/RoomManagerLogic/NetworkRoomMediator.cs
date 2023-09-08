using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.RoomManagerLogic
{
    public class NetworkRoomMediator : INetworkRoomMediator
    {
        public event Action OnRoomCreatingFailed;
        public event Action OnJoiningRoom;
        public event Action OnJoinedRoom;
        public event Action OnJoinRoomFailed;
        public event Action OnRoomLeaving;
        public event Action OnRoomLeaved;
        public event Action OnOpponentLeavedRoom;
        public event Action<List<RoomInfo>> OnRoomListUpdated;

        public void NotifyAboutJoiningRoom()
        {
            OnJoiningRoom?.Invoke();
        }
        
        public void NotifyAboutSuccessfulJoiningRoom()
        {
            OnJoinedRoom?.Invoke();
        }
        
        public void NotifyAboutFailedJoiningRoom()
        {
            OnJoinRoomFailed?.Invoke();
        }

        public void NotifyAboutRoomListUpdating(List<RoomInfo> roomInfos)
        {
            OnRoomListUpdated?.Invoke(roomInfos);
        }

        public void NotifyAboutLeavingRoom()
        {
            OnRoomLeaving?.Invoke();
        }

        public void NotifyAboutSuccessfulLeavingRoom()
        {
            OnRoomLeaved?.Invoke();
        }

        public void NotifyAboutOpponentLeaving()
        {
            OnOpponentLeavedRoom?.Invoke();
        }

        public void NotifyAboutFailedRoomCreating()
        {
            OnRoomCreatingFailed?.Invoke();
        }
    }
}