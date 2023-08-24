using System;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsComander : IUnitsComander
    {
        private readonly ITurnManager _turnManager;
        private readonly IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        private readonly IUnitMoveValidator _unitMoveValidator;
        private readonly IUnitSelectValidator _unitSelectValidator;
        private readonly IUnitsStateContainer _unitsStateContainer;
        private readonly IUnitsPlacementHandler _unitsPlacementHandler;
        private readonly IBoardTilesContainer _boardTilesContainer;
        private IUnitsComanderMediator _comanderMediator;
        
        public UnitsComander(IUnitsComanderMediator comanderMediator, ITurnManager turnManager,IUnitsStateContainer unitsStateContainer, IUnitsPathCalculatorsManager unitsPathCalculatorsManager,
            IUnitMoveValidator unitMoveValidator,IUnitSelectValidator unitSelectValidator, IUnitsPlacementHandler unitsPlacementHandler, IBoardTilesContainer boardTilesContainer)
        {
            _turnManager = turnManager;
            _unitsStateContainer = unitsStateContainer;
            _unitsPathCalculatorsManager = unitsPathCalculatorsManager;
            _unitMoveValidator = unitMoveValidator;
            _unitSelectValidator = unitSelectValidator;
            _unitsPlacementHandler = unitsPlacementHandler;
            _boardTilesContainer = boardTilesContainer;
            _comanderMediator = comanderMediator;
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