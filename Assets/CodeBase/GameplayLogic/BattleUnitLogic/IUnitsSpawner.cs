using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsSpawner : IService
    {
        Task InitializeUnits();
        void PrepareUnits();
    }
}