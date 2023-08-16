using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComander
    {
        void SelectUnit(BattleUnit unit);
        void MoveUnit(BattleUnit unit ,Vector2Int newIndex);
    }
}