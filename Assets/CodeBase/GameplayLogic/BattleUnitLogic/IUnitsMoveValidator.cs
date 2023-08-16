using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsMoveValidator
    {
        bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex);
    }
}