using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public interface ITurnManagerMediator : IService
    {
        event Action<TeamType> OnTeamOfTurnChanged;

        void Notify(TeamType teamType);
    }
}