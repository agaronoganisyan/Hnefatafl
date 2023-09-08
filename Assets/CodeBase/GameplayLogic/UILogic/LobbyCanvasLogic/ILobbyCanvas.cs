using System.Threading.Tasks;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public interface ILobbyCanvas 
    {
        Task Initialize();
        void ClosePanel();
    }
}