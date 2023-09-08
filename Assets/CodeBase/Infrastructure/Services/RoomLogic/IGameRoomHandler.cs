using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public interface IGameRoomHandler : IService
    {
        IGameRoomHandlerMediator Mediator { get; }
        void TryToStartGame();
        void Quit();
    }
}