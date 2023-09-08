using CodeBase.GameplayLogic.BattleUnitLogic;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace CodeBase.NetworkLogic.PlayerLogic
{
    public class NetworkPlayerManager : INetworkPlayerManager
    {
        private TeamType _localPlayerTeamType;
        
        public void Initialize()
        {
            
        }
        
        public Player GetLocalPlayer()
        {
            return PhotonNetwork.LocalPlayer;
        }
        
        public void SetPlayerTeam(Player player,TeamType teamType)
        {
            player.SetCustomProperties(new Hashtable
            {
                {NetworkConstValues.KEY_TEAM,teamType}
            });
            
            if (IsThisPlayerIsLocalPlayer(player)) _localPlayerTeamType = teamType;
        }

        public TeamType GetPlayerTeam(Player player)
        {
            if (IsThisPlayerIsLocalPlayer(player))
            {
                return _localPlayerTeamType;
            }
            else
            {
                if (player.CustomProperties.TryGetValue(NetworkConstValues.KEY_TEAM, out var property))
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
    }
}