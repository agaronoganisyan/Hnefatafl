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
        private const int MAX_PLAYERS_AMOUNT = 2;
        
        private const byte SELECT_UNIT_EVENT_CODE  = 1;
        private const byte MOVE_UNIT_EVENT_CODE  = 2;
        private const byte TRY_TO_START_GAME_EVENT_CODE  = 3;
        
        private INetworkManagerMediator _networkManagerMediator;
        public INetworkManagerMediator NetworkManagerMediator => _networkManagerMediator;
        
        public void Initialize()
        {
            _networkManagerMediator = new NetworkManagerMediator();
            PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializableVector2Int.SerializeVector2Int, SerializableVector2Int.DeserializeVector2Int);
        }
        
        public void ConnectToServer()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            
            _networkManagerMediator.NotifyAboutChangingConnectionStatus("ConnectToServer");
        }

        public void SelectPlayerTeam(TeamType teamType)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                {KEY_TEAM,teamType}
            });
        }

        public TeamType GetPlayerTeam()
        {
            TeamType selectedTeam = TeamType.None;
            
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(KEY_TEAM, out var property))
            {
                selectedTeam = (TeamType)property;
            }

            return selectedTeam;
        }

        public bool IsConnected()
        {
            return PhotonNetwork.IsConnected;
        }

        public TeamType IsInCurrentRoomTeamWasSelected()
        {
            TeamType selectedTeam = TeamType.None;

            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                var firstPlayer = PhotonNetwork.CurrentRoom.GetPlayer(1);
                if (firstPlayer.CustomProperties.TryGetValue(KEY_TEAM, out var property))
                {
                    selectedTeam = (TeamType)property;
                }
            }

            return selectedTeam;
        }

        public bool IsCurrentRoomFull()
        {
            return PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        public void AddCallbackTarget(object target)
        {
            PhotonNetwork.AddCallbackTarget(target);
        }

        public void RaiseSelectUnitEvent(Vector2Int index)
        {
            object[] content = new object[] { new Vector2Int(index.x, index.y) };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(SELECT_UNIT_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void RaiseMoveUnitEvent(Vector2Int index)
        {
            object[] content = new object[] { new Vector2Int(index.x, index.y) };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(MOVE_UNIT_EVENT_CODE, content, raiseEventOptions, SendOptions.SendReliable);
        }

        public void RaiseTryToStartGameEvent()
        {
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others }; 
            PhotonNetwork.RaiseEvent(TRY_TO_START_GAME_EVENT_CODE,null, raiseEventOptions, SendOptions.SendReliable);
        }

        public NetworkEventType GetNetworkEventType(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;

            if (eventCode == SELECT_UNIT_EVENT_CODE) return NetworkEventType.SelectUnit;
            else if (eventCode == MOVE_UNIT_EVENT_CODE) return NetworkEventType.MoveUnit;
            else if (eventCode == TRY_TO_START_GAME_EVENT_CODE) return NetworkEventType.TryToStartGame;

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
        
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MAX_PLAYERS_AMOUNT });
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            string message = "OnConnectedToMaster()";
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);
            _networkManagerMediator.NotifyAboutConnecting();
            
            Debug.Log("OnConnectedToMaster() was called by PUN");
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
            _networkManagerMediator.NotifyAboutJoiningRoom();
 
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
            _networkManagerMediator.NotifyAboutChangingConnectionStatus(message);
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
        
        #endregion
    }
}
