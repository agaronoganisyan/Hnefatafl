using System.Threading.Tasks;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardHighlight
    {
        void Initialize(IRuleManager ruleManager, IAssetsProvider assetsProvider,
            IUnitsPathCalculatorsManager unitsPathCalculatorsManager, IUnitsComander unitsComander);
        Task GenerateBoardHighlight(int boardSize);
    }
}