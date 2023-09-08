using System;

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