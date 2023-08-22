using System.Threading.Tasks;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsSpawner
    {
        Task Initialize();
        void PrepareUnits();
    }
}