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
        Board _board;

        BattleUnit _intsUnit;
        BattleUnit _selectedUnit;

        BattleUnit[,] _units;

        int _boardSize;

        public static event Action<BattleUnit> OnSelectedUnitMovesCalculated;

        public void Initialize(Board board)
        {
            _board = board;

            _boardSize = ConstValues.BOARD_SIZE;
            _units = new BattleUnit[_boardSize, _boardSize];
        }

        public void SpawnUnits()
        {
            //Upper side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(3, 0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(4, 0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(5, 0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(6, 0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(7, 0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(5, 1));

            //Left side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(0, 3));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(0, 4));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(0, 5));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(0, 6));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(0, 7));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(1, 5));

            //Right side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize-1, 3));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize - 1, 4));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize - 1, 5));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize - 1, 6));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize - 1, 7));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(_boardSize - 2, 5));

            //Bottom side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(3, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(4,  _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(5,  _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(6,  _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(7,  _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector2Int(5,  _boardSize - 2));

            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(3, 5));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(4, 4));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(4, 5));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(4, 6));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(5, 3));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(5, 4));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(5, 6));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(5, 7));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(6, 4));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(6, 5));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(6, 6));
            InstantiateUnit(BattleUnitType.Defender, new Vector2Int(7, 5));

            float boardSizeHalf = (float)_boardSize / 2 - 0.5f;
            InstantiateUnit(BattleUnitType.King, new Vector2Int((int)boardSizeHalf,  (int)boardSizeHalf));
        }

        public void InstantiateUnit(BattleUnitType battleUnitType, Vector2Int index)
        {
            _intsUnit = Instantiate(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(battleUnitType)));
            _intsUnit.Initialize(index);
            AddUnitToTile(_intsUnit, index);
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
            _selectedUnit.CalculateAvailableMoves( _board, this);
            OnSelectedUnitMovesCalculated?.Invoke(_selectedUnit);
        }

        void PlaceUnit(Tile finalTile)
        {
            RemoveUnitFromTile(_selectedUnit);
            AddUnitToTile(_selectedUnit, finalTile.Index);
        }

        void AddUnitToTile(BattleUnit unit, Vector2Int pos)
        {
            _units[pos.x, pos.y] = unit;
            unit.SetPosition(pos);
        }

        void RemoveUnitFromTile(BattleUnit unit)
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