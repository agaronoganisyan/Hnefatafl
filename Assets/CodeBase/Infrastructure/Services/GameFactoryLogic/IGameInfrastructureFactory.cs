using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;

namespace CodeBase.Infrastructure.Services.GameFactoryLogic
{
    public interface IGameInfrastructureFactory : IService
    {
        Task<NetworkManager> CreateNetworkManager();
        Task CreateBoard();
        Task CreateBoardHighlight();
        Task CreateGameplayCanvas();
        Task CreateDebriefingCanvas();
        Task CreateLobbyCanvas();
    }
}