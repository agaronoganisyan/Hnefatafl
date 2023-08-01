using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic
{
    public class GameplayPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _currentTeamOfTurn;

        public void Initialize()
        {
            Controller.OnCurrentTeamOfTurnChanged += SetTeamOfTurn;
        }

        void SetTeamOfTurn(TeamType teamType)
        {
            _currentTeamOfTurn.text = $"{teamType} team";
        }
    }
}