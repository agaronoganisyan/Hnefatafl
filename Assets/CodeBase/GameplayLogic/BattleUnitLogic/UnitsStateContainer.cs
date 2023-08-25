using CodeBase.GameplayLogic.TileLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsStateContainer : IUnitsStateContainer
    {
    BattleUnit[,] _units;

    private int _boardSize;

    public void Initialize()
    {
        GameModeStaticData currentModeData =
            ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);

        _boardSize = currentModeData.BoardSize;
    }

    public void GenerateContainer()
    {
        _units = new BattleUnit[_boardSize, _boardSize];
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

    public void ChangeUnitIndex(BattleUnit unit, Vector2Int newIndex)
    {
        RemoveUnitFromTile(unit);
        AddUnitToTile(unit,newIndex);
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