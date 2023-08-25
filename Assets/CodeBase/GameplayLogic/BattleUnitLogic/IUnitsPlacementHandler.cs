using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsPlacementHandler : IService
    {
        void ProcessPlacement(BattleUnit placedUnit, TileType finalTileType);
    }
}