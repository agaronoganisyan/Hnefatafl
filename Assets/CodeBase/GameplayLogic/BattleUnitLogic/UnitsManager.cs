using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsManager : MonoBehaviour, IService
    {
        BattleUnit _intsUnit;

        BattleUnit[,] _units;

        int _boardSize;

        public void Initialize()
        {
            _boardSize = ConstValues.BOARD_SIZE;
            _units = new BattleUnit[_boardSize, _boardSize];
        }

        public void SpawnUnits()
        {
            //Upper side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(3, 0,0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(4, 0,0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(5, 0,0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(6, 0,0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(7, 0,0));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(5, 0,1));

            //Left side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(0, 0,3));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(0, 0,4));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(0, 0,5));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(0, 0,6));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(0, 0,7));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(1, 0,5));

            //Right side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize-1, 0,3));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize - 1, 0,4));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize - 1, 0,5));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize - 1, 0,6));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize - 1, 0,7));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(_boardSize - 2, 0,5));

            //Bottom side attackers
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(3, 0, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(4, 0, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(5, 0, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(6, 0, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(7, 0, _boardSize - 1));
            InstantiateUnit(BattleUnitType.Attacker, new Vector3(5, 0, _boardSize - 2));

            InstantiateUnit(BattleUnitType.Defender, new Vector3(3, 0,5));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(4, 0,4));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(4, 0,5));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(4, 0,6));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(5, 0,3));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(5, 0,4));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(5, 0,6));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(5, 0,7));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(6, 0,4));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(6, 0,5));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(6, 0,6));
            InstantiateUnit(BattleUnitType.Defender, new Vector3(7, 0,5));

            float boardSizeHalf = (float)_boardSize / 2 - 0.5f;
            InstantiateUnit(BattleUnitType.King, new Vector3(boardSizeHalf, 0, boardSizeHalf));
        }

        public void InstantiateUnit(BattleUnitType battleUnitType, Vector3 pos)
        {
            _intsUnit = Instantiate(AssetsProvider.GetCachedAsset<BattleUnit>(AssetsPath.PathToBattleUnit(battleUnitType)));
            _intsUnit.Initialize(pos);
            _units[(int)pos.x, (int)pos.z] = _intsUnit;
        }

        public bool IsThereUnit(Vector3 pos)
        {
            return _units[(int)pos.x, (int)pos.z] != null ? true : false;
        }
    }
}