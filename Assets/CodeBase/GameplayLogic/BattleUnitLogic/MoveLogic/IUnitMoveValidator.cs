using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public interface IUnitMoveValidator : IService
    {
        bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex);
    }
}