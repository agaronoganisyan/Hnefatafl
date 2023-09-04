using System.Threading.Tasks;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.RuleManagerLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.NetworkLogic.RoomLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsSpawner : IUnitsSpawner
    {
    private IUnitsFactory _unitsFactory;    
    private IUnitsStateContainer _unitsStateContainer;
    private ITeamsUnitsContainer _teamsUnitsContainer;
    private int _boardSize;
        
    public void Initialize()
    {
        GameModeStaticData currentModeData =
            ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);
        
        _unitsFactory = ServiceLocator.Get<IUnitsFactory>();
        _unitsStateContainer =  ServiceLocator.Get<IUnitsStateContainer>();
        _teamsUnitsContainer =  ServiceLocator.Get<ITeamsUnitsContainer>();
        _boardSize = currentModeData.BoardSize;

        //ServiceLocator.Get<IRuleManager>().RuleManagerMediator.OnGameRestarted += Restart;
        ServiceLocator.Get<IGameRoomHandler>().GameRoomHandlerMediator.OnQuitRoom += Restart;
    }
    
    public async Task InitializeUnits()
    {
        await _unitsFactory.InitializePool();
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
