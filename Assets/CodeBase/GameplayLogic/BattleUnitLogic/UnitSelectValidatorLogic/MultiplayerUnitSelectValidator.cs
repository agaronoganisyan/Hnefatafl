using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic
{
    public class MultiplayerUnitSelectValidator : UnitSelectValidator
    {
        private INetworkManager _networkManager;
        
        public override void Initialize()
        {
            base.Initialize();

            _networkManager = ServiceLocator.Get<INetworkManager>();
        }

        public override BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index)
        {
            BattleUnit selectedUnit = _unitsStateContainer.GetUnitByIndex(index);

            if (selectedUnit == null || selectedUnit.TeamType != turnManager.TeamOfTurn || turnManager.TeamOfTurn != _networkManager.GetPlayerTeam()) return null;
            else return selectedUnit;
        }
    }
}