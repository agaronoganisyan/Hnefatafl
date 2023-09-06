using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic
{
    public abstract class UnitsCommander : IUnitsCommander
    {
        public IUnitsCommanderMediator CommanderMediatorMediator => _commanderMediatorMediator;
        private IUnitsCommanderMediator _commanderMediatorMediator;
        
        private ITurnManager _turnManager;
        private IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        private IUnitMoveValidator _unitMoveValidator;
        private IUnitSelectValidator _unitSelectValidator;
        private IUnitsStateContainer _unitsStateContainer;
        private IUnitsPlacementHandler _unitsPlacementHandler;
        private IBoardTilesContainer _boardTilesContainer;
        
        public virtual void Initialize()
        {
            _turnManager = ServiceLocator.Get<ITurnManager>();
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
            _unitsPathCalculatorsManager = ServiceLocator.Get<IUnitsPathCalculatorsManager>();
            _unitMoveValidator = ServiceLocator.Get<IUnitMoveValidator>();
            _unitSelectValidator = ServiceLocator.Get<IUnitSelectValidator>();
            _unitsPlacementHandler = ServiceLocator.Get<IUnitsPlacementHandler>();
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            
            _commanderMediatorMediator = new UnitsCommanderMediator();
        }
        
        public virtual void SelectUnit(Vector2Int index)
        {
            BattleUnit selectedUnit = _unitSelectValidator.TryToSelectUnit(_turnManager,index);

            if (selectedUnit == null) return;
            
            _turnManager.SelectUnit(selectedUnit,_unitsPathCalculatorsManager.CalculatePath(selectedUnit));
            
            _commanderMediatorMediator.NotifyAboutSelectedUnit(); 
        }

        public virtual void MoveUnit(Vector2Int newIndex)
        {
            if (_unitMoveValidator.IsUnitCanMove(_turnManager.SelectedUnitPath, newIndex))
            {
                _unitsStateContainer.ChangeUnitIndex(_turnManager.SelectedUnit,newIndex);
                _unitsPlacementHandler.ProcessPlacement(_turnManager.SelectedUnit, _boardTilesContainer.GetTileTypeByIndex(newIndex));
                
                _turnManager.SwitchTeamOfTurn();
            }
            else
            {
                _turnManager.UnselectUnit();
            }
            
            _commanderMediatorMediator.NotifyAboutUnselectedUnit();
        }
    }
}