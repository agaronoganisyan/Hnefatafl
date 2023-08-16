using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardHighlight
    {
        public void Initialize(IUnitsPathCalculatorsManager unitsPathCalculatorsManager);
        void GenerateBoardHighlight(int boardSize);
    }
}