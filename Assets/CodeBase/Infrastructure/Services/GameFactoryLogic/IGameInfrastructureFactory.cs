using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.GameFactoryLogic
{
    public interface IGameInfrastructureFactory : IService
    {
        Task CreateBoard();
        Task CreateBoardHighlight();
        Task CreateGameplayCanvas();
        Task CreateDebriefingCanvas();
    }
}