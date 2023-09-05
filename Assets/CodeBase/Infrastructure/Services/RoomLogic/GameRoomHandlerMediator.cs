using System;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public class GameRoomHandlerMediator : IGameRoomHandlerMediator
    {
        public event Action OnQuitRoom;
        
        public void NotifyAboutQuittingRoom()
        {
            OnQuitRoom?.Invoke();
        }
    }
}