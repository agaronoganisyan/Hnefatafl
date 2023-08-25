using System;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsComander : IUnitsComander
    {
        private ITurnManager _turnManager;
        private IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        private IUnitMoveValidator _unitMoveValidator;
        private IUnitSelectValidator _unitSelectValidator;
        private IUnitsStateContainer _unitsStateContainer;
        private IUnitsPlacementHandler _unitsPlacementHandler;
        private IBoardTilesContainer _boardTilesContainer;
        private IUnitsComanderMediator _comanderMediator;
        
        public void Initialize()
        {
            _turnManager = ServiceLocator.Get<ITurnManager>();
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
            _unitsPathCalculatorsManager = ServiceLocator.Get<IUnitsPathCalculatorsManager>();
            _unitMoveValidator = ServiceLocator.Get<IUnitMoveValidator>();
            _unitSelectValidator = ServiceLocator.Get<IUnitSelectValidator>();
            _unitsPlacementHandler = ServiceLocator.Get<IUnitsPlacementHandler>();
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _comanderMediator = ServiceLocator.Get<IUnitsComanderMediator>();
        }
        
        public void SelectUnit(Vector2Int index)
        {
            BattleUnit selectedUnit = _unitSelectValidator.TryToSelectUnit(_turnManager,index);

            if (selectedUnit == null) return;
            
            _turnManager.SelectUnit(selectedUnit,_unitsPathCalculatorsManager.CalculatePath(selectedUnit));
            
            _comanderMediator.NotifyAboutSelectedUnit(); 
        }

        public void MoveUnit(Vector2Int newIndex)
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
            
            _comanderMediator.NotifyAboutUnselectedUnit();
        }
    }
}