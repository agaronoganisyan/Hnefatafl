using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.TileLogic;
using System;
using CodeBase.GameplayLogic.BoardLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsManager : MonoBehaviour, IService
    {
        UnitsSpawner _unitsSpawner;
        KillsHandler _killsHandler;

        GameManager _gameManager;
        Board _board;

        BattleUnit _selectedUnit;

        BattleUnit[,] _units;

        public static event Action<BattleUnit> OnSelectedUnitMovesCalculated;

        public void Initialize(GameManager gameManager, Board board)
        {
            _gameManager = gameManager;
            _board = board;

            _unitsSpawner = new UnitsSpawner(_board, this, ConstValues.BOARD_SIZE);
            _killsHandler = new KillsHandler(_gameManager, _board, this);

            _units = new BattleUnit[ConstValues.BOARD_SIZE, ConstValues.BOARD_SIZE];
            _unitsSpawner.PrepareUnits(true);
        }

        public void Restart()
        {
            _units = new BattleUnit[ConstValues.BOARD_SIZE, ConstValues.BOARD_SIZE];
            _unitsSpawner.Restart();
        }

        public BattleUnit GetUnitByIndex(Vector2Int index)
        {
            if (IsThereUnit(index)) return _units[index.x, index.y];
            else return null;
        }

        public bool IsThereUnit(Vector2Int index)
        {
            return _units[index.x, index.y] != null ? true : false;
        }

        void SelectUnit(BattleUnit selectedUnit)
        {
            _selectedUnit = _units[selectedUnit.Index.x, selectedUnit.Index.y];
            _selectedUnit.CalculateAvailableMoves();
            OnSelectedUnitMovesCalculated?.Invoke(_selectedUnit);
        }

        void PlaceUnit(Tile finalTile)
        {
            RemoveUnitFromTile(_selectedUnit);
            AddUnitToTile(_selectedUnit, finalTile.Index);

            _killsHandler.TryToKill(_selectedUnit, finalTile);
            if (_selectedUnit.UnitType == UnitType.King && finalTile.Type == TileType.Shelter) _gameManager.WinGame();
        }

        public void AddUnitToTile(BattleUnit unit, Vector2Int pos)
        {
            _units[pos.x, pos.y] = unit;
            unit.SetPosition(pos);
        }

        public void RemoveUnitFromTile(BattleUnit unit)
        {
            _units[unit.Index.x, unit.Index.y] = null;
        }

        private void OnEnable()
        {
            Controller.OnUnitSelected += SelectUnit;
            Controller.OnUnitPlaced += PlaceUnit;
        }

        private void OnDisable()
        {
            Controller.OnUnitSelected -= SelectUnit;
            Controller.OnUnitPlaced -= PlaceUnit;
        }
    }
}