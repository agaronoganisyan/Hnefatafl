using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputHandler : IInputHandler
    {
        private IBoardTilesContainer _boardTilesContainer;
        private IUnitsStateContainer _unitsStateContainer;
        private IUnitsComander _unitsComander;
        private ITurnManager _turnManager;
        
        private Camera _camera;

        public InputHandler(IInputService inputService,ITurnManager turnManager, IUnitsComander unitsComander,
            IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer)
        {
            _camera = Camera.main;
            _unitsComander = unitsComander;
            _boardTilesContainer = boardTilesContainer;
            _unitsStateContainer = unitsStateContainer;
            _turnManager = turnManager;
            
            inputService.OnClickedOnBoard += ProcessClickOnBoard;
        }

        public void ProcessClickOnBoard(Vector2 mousePosition)
        {
            if (!_boardTilesContainer.IsWorldPosOnBoard(_camera.ScreenToWorldPoint(mousePosition))) return;
            
            Vector2Int index = _boardTilesContainer.GetIndexByWorldPos(_camera.ScreenToWorldPoint(mousePosition));
            
            if (_turnManager.SelectedUnit != null)
            {
                _unitsComander.MoveUnit(index);
                
                //_selectedUnit = null;
                
                // if (_selectedUnit.IsThisIndexAvailableToMove(index))
                // {
                //     SwitchTeamOfTurn();
                //     OnUnitPlaced?.Invoke(_selectedTile);
                // }
                //
                // OnDisableHighlight?.Invoke();
            }
            else
            {
                if(!_unitsStateContainer.IsThereUnit(index)) return;
                 
                _unitsComander.SelectUnit(_unitsStateContainer.GetUnitByIndex(index));
                
                // if (_selectedUnit == null) return;
                //
                // if (_selectedUnit.TeamType != _currentTeamOfTurn)
                // {
                //     _selectedUnit = null;
                //     return;
                // }
                //
                // OnUnitSelected?.Invoke(_selectedUnit);
            }
        }
    }
}