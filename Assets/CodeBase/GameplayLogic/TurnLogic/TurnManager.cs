using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public class TurnManager : ITurnManager
    {
        public TeamType TeamOfTurn => _teamOfTurn;
        private TeamType _teamOfTurn;

        public BattleUnit SelectedUnit => _selectedUnit;
        private BattleUnit _selectedUnit;

        public IUnitPath SelectedUnitPath => _selectedUnitPath;
        public event Action<TeamType> OnTeamOfTurnChanged;
        private IUnitPath _selectedUnitPath;

        public TurnManager(IRuleManager ruleManager)
        {
            ruleManager.OnGameStarted += Prepare;
        }

        public void SwitchTeamOfTurn()
        {
            if (_teamOfTurn == TeamType.Black)_teamOfTurn = TeamType.White;
            else if (_teamOfTurn == TeamType.White) _teamOfTurn = TeamType.Black;
            
            _selectedUnit = null;
            _selectedUnitPath = null;
            
            OnTeamOfTurnChanged?.Invoke(_teamOfTurn);
        }

        public void Prepare()
        {
            _teamOfTurn = TeamType.White;
            _selectedUnit = null;
            _selectedUnitPath = null;
        }

        public void SelectUnit(BattleUnit unit, IUnitPath selectedUnitPath)
        {
            _selectedUnit = unit;
            _selectedUnitPath = selectedUnitPath;
        }

        public void UnselectUnit()
        {
            _selectedUnit = null;
            _selectedUnitPath = null;
        }
    }
}