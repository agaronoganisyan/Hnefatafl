using UnityEngine;
using TMPro;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic.RoomLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayPanel : MonoBehaviour
    {
        private IGameRoomHandler _roomHandler;
        
        [SerializeField] TextMeshProUGUI _currentTeamOfTurn;

        public void Initialize()
        {
            _roomHandler = ServiceLocator.Get<IGameRoomHandler>();

            ServiceLocator.Get<ITurnManager>().TurnManagerMediator.OnTeamOfTurnChanged += SetTeamOfTurn;
        }

        void SetTeamOfTurn(TeamType teamType)
        {
            _currentTeamOfTurn.text = $"{teamType} team";
        }

        public void QuitButton()
        {
            _roomHandler.Quit();
        }
    }
}