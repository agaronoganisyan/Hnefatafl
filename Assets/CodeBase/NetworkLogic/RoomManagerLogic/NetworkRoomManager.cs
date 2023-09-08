using System.Collections.Generic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.LobbyLogic;
using CodeBase.NetworkLogic.ManagerLogic;
using CodeBase.NetworkLogic.PlayerLogic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CodeBase.NetworkLogic.RoomManagerLogic
{
    public class NetworkRoomManager : NetworkRoomCallbacks , INetworkRoomManager
    {
        public INetworkRoomMediator Mediator => _mediator;
        private INetworkRoomMediator _mediator;

        private INetworkManager _networkManager;
        private INetworkPlayerManager _networkPlayerManager;
        
        private const int MAX_PLAYERS_AMOUNT = 2;
        
        private List<RoomInfo> _cachedRoomList = new List<RoomInfo>();

        public void Initialize()
        {
            _mediator = new NetworkRoomMediator();

            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkPlayerManager = ServiceLocator.Get<INetworkPlayerManager>();
            
            _networkManager.AddCallbackTarget(this);
        }
        
        void DeleteRoom(Room room)
        {
            room.IsOpen = false;
            room.IsVisible = false;
            room.RemovedFromList = true;
            
            if (_cachedRoomList.Contains(room)) _cachedRoomList.Remove(room);
        }

        public void MarkRoomAsGameStarted(Room room)
        {
            // room.SetCustomProperties(new Hashtable
            // {
            //     {KEY_ROOM_GAME_IS_STARTED,true}
            // });

            DeleteRoom(room);
        }

        public void SelectTeamInRoom(TeamType teamType,Room room)
        {
            room.SetCustomProperties(new Hashtable
            {
                {NetworkConstValues.KEY_TEAM,teamType}
            });

            _networkPlayerManager.SetPlayerTeam(_networkPlayerManager.GetLocalPlayer(),teamType);
        }

        public TeamType GetSelectedTeamInRoom(Room room)
        {
            TeamType selectedTeam = TeamType.None;

            if (!IsInRoom()) return selectedTeam;
            
            Hashtable customRoomProperties = room.CustomProperties;
            
            if (customRoomProperties.TryGetValue(NetworkConstValues.KEY_TEAM, out var property))
            {
                selectedTeam = (TeamType)property;
            }

            return selectedTeam;
        }
        
        public bool IsAllPlayersInRoomSelectTeam(Room room)
        {
            if (room.PlayerCount > 1)
            {
                for (int i = 1; i <= MAX_PLAYERS_AMOUNT; i++)
                {
                    Player player = room.GetPlayer(i);
                    if (_networkPlayerManager.GetPlayerTeam(player) != TeamType.None)
                    {
                        if (i == MAX_PLAYERS_AMOUNT) return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        
        bool IsInRoom()
        {
            return PhotonNetwork.InRoom;
        }

        public Room GetCurrentRoom()
        {
            return PhotonNetwork.CurrentRoom;
        }

        public bool IsRoomFull(Room room)
        {
            return room.PlayerCount == room.MaxPlayers;
        }
        
        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MAX_PLAYERS_AMOUNT },NetworkConstValues.DefaultLobby);
            
            _mediator.NotifyAboutJoiningRoom();
            _networkManager.ChangeConnectionStatus("Join Room");
        }

        public void JoinPrescribedRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
            
            _mediator.NotifyAboutJoiningRoom();
            _networkManager.ChangeConnectionStatus("Join Room");
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
            
            _mediator.NotifyAboutJoiningRoom();
            _networkManager.ChangeConnectionStatus("Join Room");
        }

        public void LeaveRoom()
        {
            if (!IsInRoom()) return;
            
            PhotonNetwork.LeaveRoom();
            
            _mediator.NotifyAboutLeavingRoom();
            _networkManager.ChangeConnectionStatus("Leave Room");
        }
        
        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            foreach (RoomInfo roomInfo in roomList)
            {
                if (!roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList)
                {
                    if (_cachedRoomList.Contains(roomInfo)) _cachedRoomList.Remove(roomInfo);

                    continue;
                }

                if (!_cachedRoomList.Contains(roomInfo)) _cachedRoomList.Add(roomInfo);
            }
            
            foreach (RoomInfo roomInfo in _cachedRoomList)
            {
                if (!roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList)
                {
                    _cachedRoomList.Remove(roomInfo);
                }
            }
        }
        
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _mediator.NotifyAboutFailedRoomCreating();
        }
        
        public override void OnJoinedRoom()
        {
            _mediator.NotifyAboutSuccessfulJoiningRoom();
            _networkManager.ChangeConnectionStatus("Joined Room");

            _networkPlayerManager.SetPlayerTeam(_networkPlayerManager.GetLocalPlayer(),TeamType.None);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            _mediator.NotifyAboutFailedJoiningRoom();
            _networkManager.ChangeConnectionStatus("Join Room Failed");
            
            Debug.Log("Join Room Failed");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            _mediator.NotifyAboutFailedJoiningRoom();
            _networkManager.ChangeConnectionStatus("Join Random Failed");

            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one." +
                      "\nCalling: PhotonNetwork.CreateRoom");
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            UpdateCachedRoomList(roomList);
            
            _mediator.NotifyAboutRoomListUpdating(_cachedRoomList);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            _mediator.NotifyAboutOpponentLeaving();
        }

        public override void OnLeftRoom()
        {
            _mediator.NotifyAboutSuccessfulLeavingRoom();
        }
    }
}