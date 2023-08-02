using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsSpawner 
    {
        Board _board;
        UnitsManager _unitsManager;

        int _boardSize;

        CustomPool<BattleUnit> _whiteWarriorsPool;
        CustomPool<BattleUnit> _blackWarriorsPool;
        CustomPool<BattleUnit> _whiteKingsPool;

        List<BattleUnit> _allWhiteUnits = new List<BattleUnit>();
        public IReadOnlyList<BattleUnit> AllWhiteUnits => _allWhiteUnits;
        List<BattleUnit> _allBlackUnits = new List<BattleUnit>();
        public IReadOnlyList<BattleUnit> AllBlackUnits => _allBlackUnits;

        public UnitsSpawner(Board board, UnitsManager unitsManager)
        {
            _board = board;
            _unitsManager = unitsManager;

            _boardSize = ConstValues.BOARD_SIZE;
        }

        public void InitUnits()
        {
            _whiteWarriorsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.Warrior)));
            _blackWarriorsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.Black, UnitType.Warrior)));
            _whiteKingsPool = new CustomPool<BattleUnit>(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(TeamType.White, UnitType.King)));

            BattleUnit intsUnit = null;

            for (int i = 0; i < ConstValues.WHITE_WARRIORS_AMOUNT; i++)
            {
                intsUnit = _whiteWarriorsPool.Get();

                InitSingleUnit(intsUnit, TeamType.White);
            }

            for (int i = 0; i < ConstValues.WHITE_KINGS_AMOUNT; i++)
            {
                intsUnit = _whiteKingsPool.Get();

                InitSingleUnit(intsUnit, TeamType.White);
            }

            for (int i = 0; i < ConstValues.BLACK_WARRIORS_AMOUNT; i++)
            {
                intsUnit = _blackWarriorsPool.Get();

                InitSingleUnit(intsUnit, TeamType.Black);
            }

            DisableAllUnits();
        }

        void InitSingleUnit(BattleUnit unit, TeamType teamType)
        {
            unit.Initialize(_board, _unitsManager);

            if (teamType == TeamType.White) _allWhiteUnits.Add(unit);
            else _allBlackUnits.Add(unit);
        }

        public void PrepareUnits()
        {
            //Upper side attackers
            for (int i=0;i<5;i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3+i, 0));
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, 1));

            //Left side attackers
            for (int i = 0; i < 5; i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 3 + i));
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(1, 5));

            //Right side attackers
            for (int i = 0; i < 5; i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 3 + i));
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 2, 5) );

            //Bottom side attackers
            for (int i = 0; i < 5; i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3 + i,_boardSize - 1));
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, _boardSize - 2) );

            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(3, 5) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 4) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 5) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 6) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 3) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 4) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 6) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 7) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 4) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 5) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 6) );
            PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(7, 5) );

            int boardSizeHalf = (int)((float)_boardSize / 2 - 0.5f);
            PrepareSingleUnit(TeamType.White, UnitType.King, new Vector2Int(boardSizeHalf, boardSizeHalf) );
        }

        public void Restart()
        {
            DisableAllUnits();
            PrepareUnits();
        }

        public void DisableAllUnits()
        {
            int allWhiteUnitsAmount = _allWhiteUnits.Count;
            for (int i = 0; i < allWhiteUnitsAmount; i++)
            {
                _allWhiteUnits[i].SetActiveStatus(false);
            }

            int allBlackUnitsAmount = _allBlackUnits.Count;
            for (int i = 0; i < allBlackUnitsAmount; i++)
            {
                _allBlackUnits[i].SetActiveStatus(false);
            }
        }

        void PrepareSingleUnit(TeamType teamType, UnitType battleUnitType, Vector2Int index)
        {
            BattleUnit intsUnit = null;

            if (teamType == TeamType.White)
            {
                if (battleUnitType == UnitType.King) intsUnit = _whiteKingsPool.Get();
                else if (battleUnitType == UnitType.Warrior) intsUnit = _whiteWarriorsPool.Get();
            }
            else
            {
                if (battleUnitType == UnitType.Warrior) intsUnit = _blackWarriorsPool.Get();
            }

            _unitsManager.AddUnitToTile(intsUnit, index);
        }
    }
}
