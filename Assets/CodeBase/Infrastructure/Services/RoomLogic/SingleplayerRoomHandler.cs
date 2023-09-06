using UnityEngine;

namespace CodeBase.Infrastructure.Services.RoomLogic
{
    public class SingleplayerRoomHandler : GameRoomHandler
    {
        protected override bool IsGameCanBeStarted()
        {
            return true;
        } 
    }
}