using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsStateContainer : IUnitsStateContainer
    {
    BattleUnit[,] _units;

    private int _boardSize;

    public UnitsStateContainer(int boardSize)
    {
        _boardSize = boardSize;

        _units = new BattleUnit[_boardSize, _boardSize];

        GameManager.OnGameRestarted += Clear;
    }

    public void Clear()
    {
        _units = new BattleUnit[_boardSize, _boardSize];
    }

    public BattleUnit GetUnitByIndex(Vector2Int index)
    {
        return IsThereUnit(index) ? _units[index.x, index.y] : null;
    }

    public bool IsThereUnit(Vector2Int index)
    {
        return _units[index.x, index.y] != null;
    }

    public void RelocateUnit(BattleUnit unit, Vector2Int newIndex)
    {
        RemoveUnitFromTile(unit);
        AddUnitToTile(unit,newIndex);
        
        // _selectedUnit = _units[selectedUnit.Index.x, selectedUnit.Index.y];
        // _selectedUnit.CalculateAvailableMoves();
        // OnSelectedUnitMovesCalculated?.Invoke(_selectedUnit);
    }

    void PlaceUnit(Tile finalTile)
    {
        // RemoveUnitFromTile(_selectedUnit);
        // AddUnitToTile(_selectedUnit, finalTile.Index);
        //
        // _unitsPlacementHandler.ProcessUnitPlacement(_selectedUnit, finalTile);
    }

    public void AddUnitToTile(BattleUnit unit, Vector2Int pos)
    {
        _units[pos.x, pos.y] = unit;
        unit.PrepareUnit(pos);
    }

    public void RemoveUnitFromTile(BattleUnit unit)
    {
        _units[unit.Index.x, unit.Index.y] = null;
    }
    }
}