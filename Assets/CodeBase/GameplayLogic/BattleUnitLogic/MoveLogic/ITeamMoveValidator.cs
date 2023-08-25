using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public interface ITeamMoveValidator : IService
    {
        public bool IsThisTeamHaveAnyAvailableMoves(TeamType teamType);
    }
}