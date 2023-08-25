using System.Threading.Tasks;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoard
    {
        void Initialize();
        Task GenerateBoard();
    }
}