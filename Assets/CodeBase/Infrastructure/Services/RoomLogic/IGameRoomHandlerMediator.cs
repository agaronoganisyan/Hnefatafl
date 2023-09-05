using System;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public interface IGameRoomHandlerMediator
    {
        event Action OnQuitRoom;

        void NotifyAboutQuittingRoom();
    }
}