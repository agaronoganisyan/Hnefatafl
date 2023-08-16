using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsStateContainer
    {
        void RemoveUnitFromTile(BattleUnit unit);
        bool IsThereUnit(Vector2Int index);
        BattleUnit GetUnitByIndex(Vector2Int neighboringIndex);
        void AddUnitToTile(BattleUnit intsUnit, Vector2Int index);
    }
}