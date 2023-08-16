using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardHighlight
    {
        void EnableHighlight(UnitPathCalculator pathCalculator);
        void GenerateBoardHighlight(int boardSize);
    }
}