using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputHandler : IInputHandler
    {
        private BattleUnit _selectedUnit;
        private Tile _selectedTile;
        private IBoardTilesContainer _boardTilesContainer;
        private IUnitsStateContainer _unitsStateContainer;
        private IUnitsComander _unitsComander;
        
        private Camera _camera;

        public InputHandler(IInputService inputService,IUnitsComander unitsComander, IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer)
        {
            _camera = Camera.main;
            _unitsComander = unitsComander;
            _boardTilesContainer = boardTilesContainer;
            _unitsStateContainer = unitsStateContainer;
            
            inputService.OnClickedOnBoard += ProcessClickOnBoard;
        }

        public void ProcessClickOnBoard(Vector2 mousePosition)
        {
            if (!_boardTilesContainer.IsWorldPosOnBoard(_camera.ScreenToWorldPoint(mousePosition))) return;
            
            Vector2Int index = _boardTilesContainer.GetIndexByWorldPos(_camera.ScreenToWorldPoint(mousePosition));
            
            _selectedUnit = _unitsStateContainer.GetUnitByIndex(index);
            
            if (_selectedUnit != null)
            {
                _unitsComander.MoveUnit(_selectedUnit, index);
                
                _selectedUnit = null;
                
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
                _selectedUnit = _unitsStateContainer.GetUnitByIndex(index);
            
                _unitsComander.SelectUnit(_selectedUnit)
                    ;
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
            
            // if (Physics.Raycast(_ray, out _hit, 100, _tileLayer))
            // {
            //     _selectedTile = _hit.transform.GetComponent<Tile>();
            //
            //     // if (_selectedUnit != null)
            //     // {
            //     //     if (_selectedUnit.IsThisIndexAvailableToMove(_selectedTile.Index))
            //     //     {
            //     //         SwitchTeamOfTurn();
            //     //         OnUnitPlaced?.Invoke(_selectedTile);
            //     //     }
            //     //
            //     //     OnDisableHighlight?.Invoke();
            //     //
            //     //     _selectedUnit = null;
            //     // }
            //     // else
            //     // {
            //     //     _selectedUnit = _unitsStateContainer.GetUnitByIndex(_selectedTile.Index);
            //     //
            //     //     if (_selectedUnit == null) return;
            //     //
            //     //     if (_selectedUnit.TeamType != _currentTeamOfTurn)
            //     //     {
            //     //         _selectedUnit = null;
            //     //         return;
            //     //     }
            //     //
            //     //     OnUnitSelected?.Invoke(_selectedUnit);
            //     // }
            // }
        }
    }
}