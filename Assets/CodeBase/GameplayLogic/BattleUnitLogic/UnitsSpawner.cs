using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using UnityEngine;
using CodeBase.Infrastructure.Services.CustomPoolLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsSpawner : IUnitsSpawner
    {
        
    private IUnitsFactory _unitsFactory;    
    private IUnitsStateContainer _unitsStateContainer;
    
    private int _boardSize;

    public UnitsSpawner(GameModeStaticData gameModeStaticData, IUnitsFactory unitsFactory,IUnitsStateContainer unitsStateContainer)
    {
        _unitsFactory = unitsFactory;
        _unitsStateContainer = unitsStateContainer;
        
        _boardSize = gameModeStaticData.BoardSize;
    }

    public void Initialize()
    {
        _unitsFactory.Initialize();
        DisableAllUnits();
    }
    
    public void Restart()
    {
        DisableAllUnits();
        PrepareUnits();
    }

    public void PrepareUnits()
    {
        //Upper side attackers
        for (int i = 0; i < 5; i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3 + i, 0));
        PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, 1));

        //Left side attackers
        for (int i = 0; i < 5; i++) PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(0, 3 + i));
        PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(1, 5));

        //Right side attackers
        for (int i = 0; i < 5; i++)
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 1, 3 + i));
        PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(_boardSize - 2, 5));

        //Bottom side attackers
        for (int i = 0; i < 5; i++)
            PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(3 + i, _boardSize - 1));
        PrepareSingleUnit(TeamType.Black, UnitType.Warrior, new Vector2Int(5, _boardSize - 2));

        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(3, 5));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 4));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 5));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(4, 6));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 3));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 4));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 6));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(5, 7));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 4));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 5));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(6, 6));
        PrepareSingleUnit(TeamType.White, UnitType.Warrior, new Vector2Int(7, 5));

        int boardSizeHalf = (int)((float)_boardSize / 2 - 0.5f);
        PrepareSingleUnit(TeamType.White, UnitType.King, new Vector2Int(boardSizeHalf, boardSizeHalf));
    }
    
    private void DisableAllUnits()
    {
        _unitsFactory.DisableAllUnits();
    }
    
    void PrepareSingleUnit(TeamType teamType, UnitType battleUnitType, Vector2Int index)
    {
        BattleUnit intsUnit = null;

        if (teamType == TeamType.White)
        {
            if (battleUnitType == UnitType.King) intsUnit = _unitsFactory.WhiteKingsPool.Get();
            else if (battleUnitType == UnitType.Warrior) intsUnit = _unitsFactory.WhiteWarriorsPool.Get();
        }
        else
        {
            if (battleUnitType == UnitType.Warrior) intsUnit = _unitsFactory.BlackWarriorsPool.Get();
        }

        _unitsStateContainer.AddUnitToTile(intsUnit, index);
    }
    }
}
