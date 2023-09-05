using System.Collections.Generic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Helpers;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CodeBase.NetworkLogic
{
    public class NetworkManager : MonoBehaviourPunCallbacks , INetworkManager
    {
        private const string KEY_TEAM = "Team";
        private const string KEY_ROOM_GAME_IS_STARTED = "RoomGameIsStarted";
        
        private const int MAX_PLAYERS_AMOUNT = 2;
        
        private const byte SELECT_UNIT_EVENT_CODE  = 1;
        private const byte MOVE_UNIT_EVENT_CODE  = 2;
        private const byte TRY_TO_START_GAME_EVENT_CODE  = 3;
        private const byte SELECT_TEAM_EVENT_CODE  = 4;
        private const byte FINISH_GAME_EVENT_CODE  = 5;

        private TeamType _localPlayerTeamType;
        
        private TypedLobby _defaultLobby = new TypedLobby("defaultLobby", LobbyType.Default);
        
        //private const byte SERIALIZE_VECTOR2INT_CODE  = unchecked((byte)301);//242

        private List<RoomInfo> _cachedRoomList = new List<RoomInfo>();
        
        public INetworkManagerMediator NetworkManagerMediator => _networkManagerMediator;
        private INetworkManagerMediator _networkManagerMediator;
        
        public void Initialize()
        {
            _networkManagerMediator = new NetworkManagerMediator();
            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializableVector2Int.SerializeVector2Int, SerializableVector2Int.DeserializeVector2Int);
        }
        
        public void ConnectToServer()
        {
            if (IsConnected())
            {
                JoinDefaultLobby();
            }
            else
            {
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();
            
                _networkManagerMediator.NotifyAboutChangingConnectionStatus("ConnectToServer");   
                
                Debug.Log("ConnectToServer");
            }
        }

        void JoinDefaultLobby()
        {
            if (!IsInLobby())
            {
                PhotonNetwork.JoinLobby(_defaultLobby);
            
                _networkManagerMediator.NotifyAboutChangingConnectionStatus("JoinDefaultLobby");
                
                Debug.Log("JoinDefaultLobby");
            }
        }

        void DeleteRoom(Room room)
        {
            room.IsOpen = false;
            room.IsVisible = false;
            room.RemovedFromList = true;
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
                {KEY_TEAM,teamType}
            });

            SetPlayerTeam(GetLocalPlayer(),teamType);
        }

        public TeamType GetSelectedTeamInRoom(Room room)
        {
            TeamType selectedTeam = TeamType.None;

            if (!IsInRoom()) return selectedTeam;
            
            Hashtable customRoomProperties = room.CustomProperties;
            
            if (customRoomProperties.TryGetValue(KEY_TEAM, out var property))
            {
                selectedTeam = (TeamType)property;
            }

            return selectedTeam;
        }

        void SetPlayerTeam(Player player,TeamType teamType)
        {
            player.SetCustomProperties(new Hashtable
            {
                {KEY_TEAM,teamType}
            });
            
            if (IsThisPlayerIsLocalPlayer(player)) _localPlayerTeamType = teamType;
        }
        
        public bool IsAllPlayersInRoomSelectTeam(Room room)
        {
            if (room.PlayerCount > 1)
            {
                for (int i = 1; i <= 2; i++)
                {
                    Player player = room.GetPlayer(i);
                    if (GetPlayerTeam(player) != TeamType.None)
                    {
                        if (i == 2) return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public TeamType GetPlayerTeam(Player player)
        {
            if (IsThisPlayerIsLocalPlayer(player))
            {
                return _localPlayerTeamType;
            }
            else
            {
                if (player.CustomProperties.TryGetValue(KEY_TEAM, out var property))
                {
                    return (TeamType)property;
                }
                else
                {
                    return TeamType.None;
                }
            }
        }

        bool IsThisPlayerIsLocalPlayer(Player player)
        {
            return player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber;
        }

        public bool IsConnected()
        {
            return PhotonNetwork.IsConnected;
        }

        public bool IsInLobby()
        {
            return PhotonNetwork.InLobby;
        }

        bool IsInRoom()
        {
            return PhotonNetwork.InRoom;
        }

        public Player GetLocalPlayer()
        {
            return PhotonNetwork.LocalPlayer;
        }

        public Room GetCurrentRoom()
        {
            return PhotonNetwork.CurrentRoom;
        }

        public bool IsRoomFull(Room room)
        {
            return room.PlayerCount == room.MaxPlayers;
        }

        public void AddCallbackTarget(object target)
        {
            PhotonNetwork.AddCallbackTarget(target);
        }
        
        public void RaiseSelectUnitEvent(Vector2Int index)
        {
            object[] content = new object[] { index };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(SELECT_UNIT_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void RaiseMoveUnitEvent(Vector2Int index)
        {
            object[] content = new object[] { index };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(MOVE_UNIT_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void RaiseTryToStartGameEvent()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(TRY_TO_START_GAME_EVENT_CODE,null, raiseEventOptions, SendOptions.SendReliable);
        }

        public void RaiseSelectTeamEvent(TeamType teamType)
        {
            object[] content = new object[] { teamType };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(SELECT_TEAM_EVENT_CODE,content, raiseEventOptions, SendOptions.SendReliable);
        }
        
        public void RaiseFinishGameEvent(TeamType winningTeam)
        {
            object[] content = new object[] { winningTeam };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(FINISH_GAME_EVENT_CODE,content, raiseEventOptions, SendOptions.SendReliable);
        }
        
        public NetworkEventType GetNetworkEventType(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;

            if (eventCode == SELECT_UNIT_EVENT_CODE) return NetworkEventType.SelectUnit;
            else if (eventCode == MOVE_UNIT_EVENT_CODE) return NetworkEventType.MoveUnit;
            else if (eventCode == TRY_TO_START_GAME_EVENT_CODE) return NetworkEventType.TryToStartGame;
            else if (eventCode == SELECT_TEAM_EVENT_CODE) return NetworkEventType.SelectTeam;
            else if (eventCode == FINISH_GAME_EVENT_CODE) return NetworkEventType.FinishGame;

            return NetworkEventType.None;
        }

        public Vector2Int GetSelectUnitEventValue(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            return (Vector2Int)data[0];
        }

        public Vector2Int GetMoveUnitEventValue(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            return (Vector2Int)data[0];
        }
        
        public TeamType GetSelectTeamEventValue(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            return (TeamType)data[0];
        }
        
        public TeamType GetFinishGameEventValue(EventData photonEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            return (TeamType)data[0];
        }
        
        public void CreateRoom(string roomName)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = MAX_PLAYERS_AMOUNT },_defaultLobby);
        }

        public void JoinPrescribedRoom(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
            
            _networkManagerMediator.NotifyAboutJoiningRoom();
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
            
            _networkManagerMediator.NotifyAboutJoiningRoom();
        }

        public void LeaveRoom()
        {
            if (IsInRoom()) PhotonNetwork.LeaveRoom();
        }
        
        private void UpdateCachedRoomList(List<RoomInfo> roomList)
        {
            for (int i = 0; i < roomList.Count; i++)
            {
                RoomInfo roomInfo = roomList[i];
                if (!roomInfo.IsOpen || !roomInfo.IsVisible || roomInfo.RemovedFromList)
                {
                    if (_cachedRoomList.Contains(roomInfo)) _cachedRoomList.Remove(roomInfo);

                    continue;
                }

                if (!_cachedRoomList.Contains(roomInfo)) _cachedRoomList.Add(roomInfo);
            }
        }
        
        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            string message = "OnConnectedToMaster()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);
            _networkManagerMediator.NotifyAboutConnecting();
            
            Debug.Log("OnConnectedToMaster");

            JoinDefaultLobby();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            string message = "OnDisconnected()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);

            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinedRoom()
        {
            string message = "OnJoinedRoom()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);
            _networkManagerMediator.NotifyAboutSuccessfulJoiningRoom();

            SetPlayerTeam(GetLocalPlayer(),TeamType.None);
            
            Debug.Log(message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            string messsage = "OnJoinRoomFailed()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);
            _networkManagerMediator.NotifyAboutFailedJoiningRoom();
            
            Debug.Log(messsage);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            string messsage = "OnJoinRandomFailed()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(messsage);
            _networkManagerMediator.NotifyAboutFailedJoiningRoom();

            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one." +
                      "\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            string message = $"Player {newPlayer.ActorNumber} entered the room";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);

            Debug.Log(message);
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            
            _networkManagerMediator.NotifyAboutJoiningLobby();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            Debug.Log("OnRoomListUpdate");

            UpdateCachedRoomList(roomList);
            
            _networkManagerMediator.NotifyAboutRoomListUpdating(_cachedRoomList);
        }

        public override void OnLeftRoom()
        {
            _networkManagerMediator.NotifyAboutLeavingRoom();
        }

        #endregion
    }
}
