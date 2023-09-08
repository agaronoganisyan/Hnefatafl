using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.PlayerLogic
{
    public interface INetworkPlayerManager : IService
    {
        TeamType GetPlayerTeam(Player player);
        Player GetLocalPlayer();
        void SetPlayerTeam(Player player, TeamType teamType);
    }
}