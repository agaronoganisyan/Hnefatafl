namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class PlaymodeSelectionPanel : LobbyPanel
    {
        public override void Initialize(LobbyPanelsManager lobbyPanelsManager)
        {
            base.Initialize(lobbyPanelsManager);
            
            _type = LobbyPanelType.PlaymodeSelection;
        }

        public void JumpToMultiplayerPanel()
        {
            _lobbyPanelsManager.SetActivePanel(LobbyPanelType.MultiplayerPlaymode);
        }
    }
}