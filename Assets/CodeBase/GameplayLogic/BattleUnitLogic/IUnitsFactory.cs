using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsFactory : IService
    {
        CustomPool<BattleUnit> WhiteWarriorsPool { get; }
        CustomPool<BattleUnit> BlackWarriorsPool{ get; }
        CustomPool<BattleUnit> WhiteKingsPool{ get; }
        
        Task InitializePool();
    }
}