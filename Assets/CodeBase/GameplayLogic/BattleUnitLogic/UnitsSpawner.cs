using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsSpawner : IUnitsSpawner
    {
        
    private readonly IUnitsFactory _unitsFactory;    
    private readonly IUnitsStateContainer _unitsStateContainer;
    private readonly ITeamsUnitsContainer _teamsUnitsContainer;
    private readonly int _boardSize;

    public UnitsSpawner(IGameManager gameManager, IUnitsFactory unitsFactory, IUnitsStateContainer unitsStateContainer,ITeamsUnitsContainer teamsUnitsContainer, int boardSize)
    {
        _unitsFactory = unitsFactory;
        _unitsStateContainer = unitsStateContainer;
        _teamsUnitsContainer = teamsUnitsContainer;
        _boardSize = boardSize;

        gameManager.OnGameRestarted += Restart;
    }

    public void Initialize()
    {
        _unitsFactory.Initialize();
        DisableAllUnits();
    }
        
    void Restart()
    {
        DisableAllUnits();
        PrepareUnits();
    }

    public void PrepareUnits()
    {
        _unitsStateContainer.Clear();
        
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
    
     void DisableAllUnits()
    {
        int allWhiteUnitsAmount = _teamsUnitsContainer.AllWhiteUnits.Count;
        for (int i = 0; i < allWhiteUnitsAmount; i++)
        {
            _teamsUnitsContainer.AllWhiteUnits[i].SetActiveStatus(false);
        }

        int allBlackUnitsAmount = _teamsUnitsContainer.AllBlackUnits.Count;
        for (int i = 0; i < allBlackUnitsAmount; i++)
        {
            _teamsUnitsContainer.AllBlackUnits[i].SetActiveStatus(false);
        }
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