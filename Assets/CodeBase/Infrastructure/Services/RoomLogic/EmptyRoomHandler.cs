namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public class EmptyRoomHandler : GameRoomHandler
    {
        protected override bool IsGameCanBeStarted()
        {
            return false;
        } 
    }
}