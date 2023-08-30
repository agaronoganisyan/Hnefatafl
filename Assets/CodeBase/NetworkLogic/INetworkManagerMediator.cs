using System;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;
        event Action OnJoinedRoom;

        void NotifyAboutSelectedUnit(string status);

        void NotifyAboutJoiningRoom();
    }
}