using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsMoveValidator : IUnitsMoveValidator
    {
        public bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex)
        {
            return unitPath.IsPathHasIndex(newIndex);
        }
    }
}