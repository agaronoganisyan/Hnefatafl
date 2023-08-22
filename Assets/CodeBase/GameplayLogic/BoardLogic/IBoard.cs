using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoard
    {
        Task GenerateBoard(int boardSize, IBoardTilesContainer boardTilesContainer, IAssetsProvider assetsProvider);
    }
}