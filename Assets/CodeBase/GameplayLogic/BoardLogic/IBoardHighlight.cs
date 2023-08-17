using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardHighlight
    {
        public void Initialize(IGameManager gameManager,IUnitsPathCalculatorsManager unitsPathCalculatorsManager, IUnitsComander unitsComander);
        void GenerateBoardHighlight(int boardSize);
    }
}