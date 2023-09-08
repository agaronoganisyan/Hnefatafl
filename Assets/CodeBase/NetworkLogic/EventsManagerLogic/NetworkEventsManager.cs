using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Helpers;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.NetworkLogic.EventsManagerLogic
{
    public enum NetworkEventType
    {
        None,
        SelectUnit,
        MoveUnit,
        TryToStartGame,
        SelectTeam,
        FinishGame
    }
    
    public class NetworkEventsManager : INetworkEventsManager
    {
        private const byte SELECT_UNIT_EVENT_CODE  = 1;
        private const byte MOVE_UNIT_EVENT_CODE  = 2;
        private const byte TRY_TO_START_GAME_EVENT_CODE  = 3;
        private const byte SELECT_TEAM_EVENT_CODE  = 4;
        private const byte FINISH_GAME_EVENT_CODE  = 5;
        
        private const byte SERIALIZE_VECTOR2INT_CODE  = 50;
        
        public void Initialize()
        {
            PhotonPeer.RegisterType(typeof(Vector2Int), SERIALIZE_VECTOR2INT_CODE, SerializableVector2Int.SerializeVector2Int, SerializableVector2Int.DeserializeVector2Int);
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
    }
}