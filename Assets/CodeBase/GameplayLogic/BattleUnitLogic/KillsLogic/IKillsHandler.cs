using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public interface IKillsHandler : IService
    {
        void AddWayToKill(UnitType unitType, WayToKill wayToKill);
        
        void FindTargetsToKill(BattleUnit unit);
    }
}