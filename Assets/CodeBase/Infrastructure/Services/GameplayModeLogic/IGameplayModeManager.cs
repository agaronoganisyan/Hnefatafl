using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services.GameplayModeLogic
{
    public enum PlaymodeType
    {
        None,
        Singleplayer,
        Multiplayer
    }
    
    public interface IGameplayModeManager : IService
    {
        IGameplayModeManagerMediator Mediator { get; }
        void SetPlaymodeType(PlaymodeType playmodeType);
    }
}