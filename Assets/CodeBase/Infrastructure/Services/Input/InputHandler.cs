using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Input
{
    public class InputHandler : IInputHandler
    {
        private BattleUnit _selectedUnit;
        private Tile _selectedTile;
        
        private Camera _camera;

        private Ray _ray;
        private RaycastHit _hit;
        private readonly int _tileLayer = 1<<6;

        public InputHandler(IInputService inputService)
        {
            _camera = Camera.main;
            
            inputService.OnClickedOnBoard += ProcessClickOnBoard;
        }

        public void ProcessClickOnBoard(Vector2 mousePosition)
        {
            Debug.Log(mousePosition);
            _ray = _camera.ScreenPointToRay(mousePosition);

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