using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic
{
    public interface IUnitsCommander : IService
    {
        IUnitsCommanderMediator Mediator { get; }
        void SelectUnit(Vector2Int index);
        void MoveUnit(Vector2Int newIndex);
    }
}