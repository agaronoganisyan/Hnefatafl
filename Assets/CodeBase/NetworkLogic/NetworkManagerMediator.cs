using System;
using System.Collections.Generic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic
{
    public class NetworkManagerMediator : INetworkManagerMediator
    {
        public event Action<string> OnConnectionStatusChanged;
        public event Action OnConnected;
        public event Action OnJoinedLobby;
        public event Action OnJoiningRoom;
        public event Action OnJoinedRoom;
        public event Action OnJoinRoomFailed;
        public event Action<List<RoomInfo>> OnRoomListUpdated;

        public void NotifyAboutChangingConnectionStatus(string status)
        {
            OnConnectionStatusChanged?.Invoke(status);
        }
        
        public void NotifyAboutConnecting()
        {
            OnConnected?.Invoke();
        }
        
        public void NotifyAboutJoiningLobby()
        {
            OnJoinedLobby?.Invoke();
        }

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
    }
}