using System;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public interface ITurnManagerMediator
    {
        event Action<TeamType> OnTeamOfTurnChanged;

        void Notify(TeamType teamType);
    }
}