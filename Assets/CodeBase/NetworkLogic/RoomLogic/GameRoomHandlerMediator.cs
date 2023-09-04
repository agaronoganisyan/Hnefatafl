using System;

namespace CodeBase.NetworkLogic.RoomLogic
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