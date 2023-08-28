using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.NetworkLogic
{
    public class NetworkManager : MonoBehaviourPunCallbacks,INetworkManager
    {
        private INetworkManagerMediator _networkManagerMediator;
        public INetworkManagerMediator NetworkManagerMediator => _networkManagerMediator;
        
        public void Initialize()
        {
            _networkManagerMediator = new NetworkManagerMediator();
        }
        
        public void ConnectToServer()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            string message = "OnConnectedToMaster()";
            _networkManagerMediator.NotifyAboutSelectedUnit(message);
            
            Debug.Log("OnConnectedToMaster() was called by PUN");
            
            //PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            string message = "OnDisconnected()";
            _networkManagerMediator.NotifyAboutSelectedUnit(message);

            Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            string messsage = "OnJoinRandomFailed()";
            _networkManagerMediator.NotifyAboutSelectedUnit(message);

            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one." +
                      "\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        public override void OnJoinedRoom()
        {
            string message = "OnJoinedRoom()";
            _networkManagerMediator.NotifyAboutSelectedUnit(message);

            Debug.Log(message);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            string message = $"Player {newPlayer.ActorNumber} entered the room";
            _networkManagerMediator.NotifyAboutSelectedUnit(message);

            Debug.Log(message);
        }
        
        #endregion
    }
}
