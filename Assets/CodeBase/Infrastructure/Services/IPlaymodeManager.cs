using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.Infrastructure.Services
{
    public enum PlaymodeType
    {
        None,
        Singleplayer,
        Multiplayer
    }
    
    public interface IPlaymodeManager : IService
    {
        void SetPlaymodeType(PlaymodeType playmodeType);
    }
}