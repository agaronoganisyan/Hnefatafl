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
    public class UnitsManager : IUnitsManager
    {
    private UnitsStateContainer _unitsStateContainer;
    private UnitsSpawner _unitsSpawner;
    private UnitsPlacementHandler _unitsPlacementHandler;

    private GameManager _gameManager;
    IBoardTilesContainer  _board;

    private BattleUnit _selectedUnit;

    public static event Action<BattleUnit> OnSelectedUnitMovesCalculated;

    public UnitsManager()
    {
                
    }

    public void Initialize(GameManager gameManager, IBoardTilesContainer board)
    {
        _gameManager = gameManager;
        _board = board;

        //_unitsSpawner = new UnitsSpawner(_board, this);
        _unitsPlacementHandler = new UnitsPlacementHandler(_gameManager, _board, this);

        //_unitsSpawner.InitUnits();
        //_unitsSpawner.PrepareUnits();

    }

    public void Restart()
    {
        //_units = new BattleUnit[ConstValues.BOARD_SIZE, ConstValues.BOARD_SIZE];
        _unitsSpawner.Restart();
    }

    public bool IsThisTeamHaveaAnyAvailableMoves(TeamType teamType)
    {
        bool status = false;

        // if (teamType == TeamType.White)
        // {
        //     for (int i = 0; i < _unitsSpawner.AllWhiteUnits.Count; i++)
        //     {
        //         if (!_unitsSpawner.AllWhiteUnits[i].IsKilled && _unitsSpawner.AllWhiteUnits[i].IsThereAvailableMoves())
        //         {
        //             status = true;
        //             break;
        //         }
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < _unitsSpawner.AllBlackUnits.Count; i++)
        //     {
        //         if (!_unitsSpawner.AllBlackUnits[i].IsKilled && _unitsSpawner.AllBlackUnits[i].IsThereAvailableMoves())
        //         {
        //             status = true;
        //             break;
        //         }
        //     }
        // }

        return status;
    }

    // void SelectUnit(BattleUnit selectedUnit)
    // {
    //     _selectedUnit = _units[selectedUnit.Index.x, selectedUnit.Index.y];
    //     _selectedUnit.CalculateAvailableMoves();
    //     OnSelectedUnitMovesCalculated?.Invoke(_selectedUnit);
    // }
    //
    // void PlaceUnit(Tile finalTile)
    // {
    //     RemoveUnitFromTile(_selectedUnit);
    //     AddUnitToTile(_selectedUnit, finalTile.Index);
    //
    //     _unitsPlacementHandler.ProcessUnitPlacement(_selectedUnit, finalTile);
    // }

  

    private void OnEnable()
    {
        GameManager.OnGameRestarted += Restart;
        //Controller.OnUnitSelected += SelectUnit;
        //Controller.OnUnitPlaced += PlaceUnit;
    }

    private void OnDisable()
    {
        GameManager.OnGameRestarted -= Restart;
        //Controller.OnUnitSelected -= SelectUnit;
        //Controller.OnUnitPlaced -= PlaceUnit;
    }
    }
}