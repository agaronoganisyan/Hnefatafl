using System;
using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;

namespace CodeBase.Infrastructure.Services.GameplayModeLogic
{
    public interface IGameplayModeManagerMediator
    {
        event Action OnGameplayModeChanged;
        void NotifyAboutGameplayModeChanging();
    }
}