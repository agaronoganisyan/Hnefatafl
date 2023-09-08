using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace CodeBase.NetworkLogic.ManagerLogic
{
    public interface INetworkManager : IService
    {
        INetworkManagerMediator Mediator { get; }
        void ConnectToServer();
        bool IsConnected();
        void AddCallbackTarget(object target);
        void ChangeConnectionStatus(string status);
    }
}