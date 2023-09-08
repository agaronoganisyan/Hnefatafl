using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.NetworkLogic.LobbyLogic
{
    public interface INetworkLobbyManager : IService
    {
        INetworkLobbyMediator Mediator { get; }
        void JoinDefaultLobby();
        bool IsInLobby();
    }
}