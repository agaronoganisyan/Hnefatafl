using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _currentTeamOfTurn;

        public void Initialize()
        {
            ServiceLocator.Get<ITurnManager>().TurnManagerMediator.OnTeamOfTurnChanged += SetTeamOfTurn;
        }

        void SetTeamOfTurn(TeamType teamType)
        {
            _currentTeamOfTurn.text = $"{teamType} team";
        }
    }
}