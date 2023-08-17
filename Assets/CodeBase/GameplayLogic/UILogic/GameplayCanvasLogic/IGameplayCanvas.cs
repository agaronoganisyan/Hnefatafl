using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public interface IGameplayCanvas
    {
        void Initialize(IGameManager gameManager, ITurnManager turnManager);
    }
}