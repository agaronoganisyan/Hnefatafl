namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public interface ITeamMoveValidator
    {
        public bool IsThisTeamHaveAnyAvailableMoves(TeamType teamType);
    }
}