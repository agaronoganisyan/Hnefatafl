using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IGameModeStaticDataService : IService
    {
        Task LoadModeData(GameModeType gameModeType);
        GameModeStaticData GetModeData(GameModeType modeType);

    }
}