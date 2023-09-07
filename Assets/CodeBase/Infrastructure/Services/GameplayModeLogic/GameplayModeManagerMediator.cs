using System;
using CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.RuleManagerLogic;

namespace CodeBase.Infrastructure.Services.GameplayModeLogic
{
    public class GameplayModeManagerMediator : IGameplayModeManagerMediator
    {
        public event Action OnGameplayModeChanged;
        
        public void NotifyAboutGameplayModeChanging()
        {
            OnGameplayModeChanged?.Invoke();
        }
    }
}