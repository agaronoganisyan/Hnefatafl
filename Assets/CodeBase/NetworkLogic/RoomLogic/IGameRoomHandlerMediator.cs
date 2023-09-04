using System;

namespace CodeBase.NetworkLogic.RoomLogic
{
    public interface IGameRoomHandlerMediator
    {
        event Action OnQuitRoom;

        void NotifyAboutQuittingRoom();
    }
}