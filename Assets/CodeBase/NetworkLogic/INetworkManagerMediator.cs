using System;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManagerMediator
    {
        event Action<string> OnConnectionStatusChanged;

        void NotifyAboutSelectedUnit(string status);
    }
}