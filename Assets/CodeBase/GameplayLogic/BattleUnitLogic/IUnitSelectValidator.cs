using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitSelectValidator
    {
        BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index);
    }
}