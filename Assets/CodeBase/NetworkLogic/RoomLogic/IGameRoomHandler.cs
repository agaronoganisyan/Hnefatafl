using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.NetworkLogic.RoomLogic
{
    public interface IGameRoomHandler : IService
    {
        IGameRoomHandlerMediator GameRoomHandlerMediator { get; }
        void TryToStartGame();
        void Quit();
    }
}