using CodeBase.Infrastructure.Services.CustomPoolLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsFactory
    {
        CustomPool<BattleUnit> WhiteWarriorsPool { get; }
        CustomPool<BattleUnit> BlackWarriorsPool{ get; }
        CustomPool<BattleUnit> WhiteKingsPool{ get; }
        
        public void Initialize();
    }
}