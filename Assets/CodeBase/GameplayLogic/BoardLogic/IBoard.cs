namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoard
    {
        void GenerateBoard(int boardSize, IBoardTilesContainer boardTilesContainer);
    }
}