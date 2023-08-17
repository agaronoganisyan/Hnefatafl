using CodeBase.GameplayLogic.TileLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsPlacementHandler
    {
        void ProcessPlacement(BattleUnit placedUnit, TileType finalTileType);
    }
}