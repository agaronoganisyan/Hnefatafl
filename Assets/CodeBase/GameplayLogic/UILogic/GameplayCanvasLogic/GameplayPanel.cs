using UnityEngine;
using TMPro;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.GameplayModeLogic;
using CodeBase.Infrastructure.Services.RoomLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayPanel : MonoBehaviour, IGameplayModeChangingObserver
    {
        private IGameRoomHandler _roomHandler;
        
        [SerializeField] TextMeshProUGUI _currentTeamOfTurn;

        public void Initialize()
        {
            _roomHandler = ServiceLocator.Get<IGameRoomHandler>();

            ServiceLocator.Get<ITurnManager>().TurnManagerMediator.OnTeamOfTurnChanged += SetTeamOfTurn;
            
            ServiceLocator.Get<IGameplayModeManager>().Mediator.OnGameplayNodeChanged += UpdateChangedProperties;
        }

        public void UpdateChangedProperties()
        {
            _roomHandler = ServiceLocator.Get<IGameRoomHandler>();
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