using System.Threading.Tasks;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.Services.AssetManagement;

namespace CodeBase.GameplayLogic.BoardLogic
{
    public interface IBoardHighlight
    {
        public void Initialize(IRuleManagerMediator ruleManagerMediator, IAssetsProvider assetsProvider,
            IUnitsPathCalculatorsManagerMediator unitsPathCalculatorsManagerMediator,
            IUnitsComanderMediator unitsComanderMediator);

        Task GenerateBoardHighlight(int boardSize);
    }
}