using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public interface IGameRoomHandler : IService
    {
        IGameRoomHandlerMediator GameRoomHandlerMediator { get; }
        void TryToStartGame();
        void Quit();
    }
}