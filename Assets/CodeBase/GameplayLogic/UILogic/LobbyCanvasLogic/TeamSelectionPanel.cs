using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.NetworkLogic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class TeamSelectionPanel : LobbyPanel
    {
        private INetworkManager _networkManager;
        
        [SerializeField] private Button _whiteTeamButton;
        [SerializeField] private Button _blackTeamButton;

        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.TeamSelection;

            _networkManager = ServiceLocator.Get<INetworkManager>();
            _networkManager.NetworkManagerMediator.OnJoinedRoom += () => _lobbyPanelsManager.SetActivePanel(LobbyPanelType.TeamSelection);
            
            _whiteTeamButton.onClick.RemoveAllListeners();
            _blackTeamButton.onClick.RemoveAllListeners();
            
            _whiteTeamButton.onClick.AddListener(() => SelectTeam(TeamType.White));
            _blackTeamButton.onClick.AddListener(() => SelectTeam(TeamType.Black));
        }

        public override void Show()
        {
            base.Show();
            PrepareTeamSelectionOptions();
        }

        void PrepareTeamSelectionOptions()
        {
            TeamType selectedTeam = _networkManager.IsInCurrentRoomTeamWasSelected();
            
            if (selectedTeam == TeamType.None) return;

            if (selectedTeam == TeamType.White) _whiteTeamButton.interactable=false;
            else if (selectedTeam == TeamType.Black) _blackTeamButton.interactable = false;
        }

        public void SelectTeam(TeamType selectedTeamType)
        {
            _networkManager.SelectPlayerTeam(selectedTeamType);
            
            if (selectedTeamType == TeamType.White) _whiteTeamButton.interactable=false;
            else if (selectedTeamType == TeamType.Black) _blackTeamButton.interactable = false;
        }
    }
}