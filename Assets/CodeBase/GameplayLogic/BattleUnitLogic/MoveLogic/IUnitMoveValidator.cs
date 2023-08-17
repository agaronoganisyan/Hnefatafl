using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public interface IUnitMoveValidator
    {
        bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex);
    }
}