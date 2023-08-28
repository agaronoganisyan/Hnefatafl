using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.NetworkLogic
{
    public interface INetworkManager : IService
    {
        INetworkManagerMediator NetworkManagerMediator { get; }

    }
}