using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public abstract class UnitPathCalculator : IService
    {
    protected IBoardTilesContainer _boardTilesContainer;
    protected IUnitsStateContainer _unitsStateContainer;

    private int _boardSize;
    
    public void Initialize()
    {
        GameModeStaticData currentModeData =
            ServiceLocator.Get<IGameModeStaticDataService>().GetModeData(GameModeType.Classic);
        
        _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
        _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
        _boardSize = currentModeData.BoardSize;
    }
    
    public IUnitPath CalculatePaths(Vector2Int currentIndex)
    {
        UnitPath path = new UnitPath();

        path.SetCurrentIndex(currentIndex);

        //Down
        for (int i = currentIndex.y - 1; i >= 0; i--)
        {
            Vector2Int index = new Vector2Int(currentIndex.x, i);

            if (IsThereProblemWithIndex(index)) break;
            else path.AddAvailableMove(index);
        }

        //Up
        for (int i = currentIndex.y + 1; i < _boardSize; i++)
        {
            Vector2Int index = new Vector2Int(currentIndex.x, i);

            if (IsThereProblemWithIndex(index)) break;
            else path.AddAvailableMove(index);
        }

        //Left
        for (int i = currentIndex.x - 1; i >= 0; i--)
        {
            Vector2Int index = new Vector2Int(i, currentIndex.y);

            if (IsThereProblemWithIndex(index)) break;
            else path.AddAvailableMove(index);
        }

        //Right
        for (int i = currentIndex.x + 1; i < _boardSize; i++)
        {
            Vector2Int index = new Vector2Int(i, currentIndex.y);

            if (IsThereProblemWithIndex(index)) break;
            else path.AddAvailableMove(index);
        }

        return path;
    }

    protected abstract bool IsThereProblemWithIndex(Vector2Int index);

    public bool IsThereAvailableMoves(Vector2Int currentIndex)
    {
        IUnitPath unitPath = CalculatePaths(currentIndex);
        return unitPath.AvailableMoves.Count > 0;
    }
    }
}