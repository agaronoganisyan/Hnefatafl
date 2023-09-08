using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.ManagerLogic
{
    public class NetworkCallbacks : IConnectionCallbacks 
    {
        public virtual void OnConnected()
        {
        }

        public virtual void OnConnectedToMaster()
        {
        }

        public virtual void OnDisconnected(DisconnectCause cause)
        {
        }

        public virtual void OnRegionListReceived(RegionHandler regionHandler)
        {
        }

        public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public virtual void OnCustomAuthenticationFailed(string debugMessage)
        {
        }
    }
}