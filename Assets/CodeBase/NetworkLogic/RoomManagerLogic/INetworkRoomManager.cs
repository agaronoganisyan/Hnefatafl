using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.RoomManagerLogic
{
    public interface INetworkRoomManager : IService
    {
        INetworkRoomMediator Mediator { get; }
        void CreateRoom(string roomName); 
        void JoinRandomRoom();
        void JoinPrescribedRoom(string roomName); 
        void LeaveRoom(); 
        void SelectTeamInRoom(TeamType teamType,Room room); 
        void MarkRoomAsGameStarted(Room room); 
        TeamType GetSelectedTeamInRoom(Room room); 
        bool IsRoomFull(Room room); 
        bool IsAllPlayersInRoomSelectTeam(Room room); 
        Room GetCurrentRoom(); 
    }
}