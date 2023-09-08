using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public class TurnManager : ITurnManager, IGameplayModeChangingObserver
    {
        private IRuleManager _ruleManager;
        
        public ITurnManagerMediator TurnManagerMediator => _managerMediator;
        private ITurnManagerMediator _managerMediator;
        
        public TeamType TeamOfTurn => _teamOfTurn;
        private TeamType _teamOfTurn;

        public BattleUnit SelectedUnit => _selectedUnit;
        private BattleUnit _selectedUnit;

        public IUnitPath SelectedUnitPath => _selectedUnitPath;
        private IUnitPath _selectedUnitPath;
        
        public void Initialize()
        {
            _managerMediator = new TurnManagerMediator();

            _ruleManager = ServiceLocator.Get<IRuleManager>();
            _ruleManager.RuleManagerMediator.OnGameStarted += Prepare;

            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayModeChanged += UpdateChangedProperties;
        }
        
        public void UpdateChangedProperties()
        {
            _ruleManager.RuleManagerMediator.OnGameStarted -= Prepare;
            
            _ruleManager = ServiceLocator.Get<IRuleManager>();
            
            _ruleManager.RuleManagerMediator.OnGameStarted += Prepare;
        }
        
        public void SwitchTeamOfTurn()
        {
            if (_teamOfTurn == TeamType.Black)_teamOfTurn = TeamType.White;
            else if (_teamOfTurn == TeamType.White) _teamOfTurn = TeamType.Black;
            
            _selectedUnit = null;
            _selectedUnitPath = null;
            
            _managerMediator.Notify(_teamOfTurn);
        }

        public void Prepare()
        {
            _teamOfTurn = TeamType.White;
            _selectedUnit = null;
            _selectedUnitPath = null;
            
            _managerMediator.Notify(_teamOfTurn);
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