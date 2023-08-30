using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic
{
    public interface IUnitSelectValidator : IService
    {
        BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index);
    }
}