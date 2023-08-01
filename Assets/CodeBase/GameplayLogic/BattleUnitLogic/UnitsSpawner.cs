using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;
using CodeBase.Infrastructure.Services.CustomPoolLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsSpawner 
    {
        Board _board;
        UnitsManager _unitsManager;

        BattleUnit _intsUnit;

        int _boardSize;

        CustomPool<BattleUnit> _whiteWarriorsPool;
        CustomPool<BattleUnit> _blackWarriorsPool;
        CustomPool<BattleUnit> _whiteKingsPool;

        List<BattleUnit> _allUnits = new List<BattleUnit>();

        public UnitsSpawner(Board board, UnitsManager unitsManager, int boardSize)
        {
            _board = board;
            _unitsManager = unitsManager;

            _boardSize = boardSize;

            _whiteWarriorsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.Warrior)));
            _blackWarriorsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.Black, UnitType.Warrior)));
            _whiteKingsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.King)));
        }

        public void PrepareUnits(bool isInit)
        {
            //Upper side attackers
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3, 0), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(4, 0), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, 0), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(6, 0), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(7, 0), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, 1), isInit);

            //Left side attackers
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 3), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 4), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 5), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 6), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 7), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(1, 5), isInit);

            //Right side attackers
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 3), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 4), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 5), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 6), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 7), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 2, 5), isInit);

            //Bottom side attackers
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3, _boardSize - 1), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(4, _boardSize - 1), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, _boardSize - 1), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(6, _boardSize - 1), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(7, _boardSize - 1), isInit);
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, _boardSize - 2), isInit);

            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(3, 5), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 4), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 5), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 6), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 3), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 4), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 6), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 7), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 4), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 5), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 6), isInit);
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(7, 5), isInit);

            float boardSizeHalf = (float)_boardSize / 2 - 0.5f;
            PrepareSingleUnit(TeamType.White, UnitType.King, new Vector2Int((int)boardSizeHalf, (int)boardSizeHalf), isInit);
        }

        public void Restart()
        {
            DisableAllUnits();
            PrepareUnits(false);
        }

        public void DisableAllUnits()
        {
            int allUnitsAmount = _allUnits.Count;
            for (int i = 0; i < allUnitsAmount; i++)
            {
                _allUnits[i].SetActiveStatus(false);
            }
        }

        void PrepareSingleUnit(TeamType teamType, UnitType battleUnitType, Vector2Int index, bool isInit)
        {
            if (teamType == TeamType.White)
            {
                if (battleUnitType == UnitType.King) _intsUnit = _whiteKingsPool.Get();
                else if (battleUnitType == UnitType.Warrior) _intsUnit = _whiteWarriorsPool.Get();
            }
            else
            {
                if (battleUnitType == UnitType.Warrior) _intsUnit = _blackWarriorsPool.Get();
            }

            if (isInit)
            {
                _intsUnit.Initialize(index, _board, _unitsManager);
                _allUnits.Add(_intsUnit);

                _unitsManager.AddUnitToTile(_intsUnit, index);
            }
            else
            {
                _unitsManager.AddUnitToTile(_intsUnit, index);
            }
        }
    }
}
