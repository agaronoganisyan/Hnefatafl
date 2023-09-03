using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.NetworkLogic.RoomLogic
{
    public interface IGameRoomHandler : IService
    {
        void TryToStartGame();

        void Quit();
    }
}