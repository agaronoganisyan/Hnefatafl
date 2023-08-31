namespace CodeBase.NetworkLogic.RoomLogic
{
    public class SingleplayerRoomHandler : GameRoomHandler
    {
        protected override bool IsGameCanBeStarted()
        {
            return true;
        }
    }
}