using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameplayLogic.UILogic.LobbyCanvasLogic
{
    public class LobbyPanelsManager  : MonoBehaviour
    {
        private LobbyPanel _activePanel;
        
        [SerializeField] private LobbyPanel[] _allPanels;
        
        private Dictionary<LobbyPanelType, LobbyPanel> _lobbyDictionary = new Dictionary<LobbyPanelType, LobbyPanel>();
        private List<LobbyPanelType> _panelsHistory = new List<LobbyPanelType>();
        
        public void Initialize()
        {
            foreach (LobbyPanel panel in _allPanels)
            {
                if (_allPanels == null) continue;
                
                panel.Initialize(this);
                
                if (_lobbyDictionary.ContainsKey(panel.Type)) continue;
                
                _lobbyDictionary.Add(panel.Type, panel);
            }

            foreach (LobbyPanelType panelKey in _lobbyDictionary.Keys)
            {
                _lobbyDictionary[panelKey].Hide();
            }

            SetActivePanel(LobbyPanelType.PlaymodeSelection);
        }

        public void SetActivePanel(LobbyPanelType newPanelType, bool isJumpingBack = false)
        {
            if (!_lobbyDictionary.ContainsKey(newPanelType)) return;
            
            if (_activePanel != null) _activePanel.Hide();

            _activePanel = _lobbyDictionary[newPanelType];
            _activePanel.Show();
            
            if (!isJumpingBack) _panelsHistory.Add(newPanelType);
        }

        public void JumpBack()
        {
            if (_panelsHistory.Count <= 1)
            {
                SetActivePanel(LobbyPanelType.PlaymodeSelection);
            }
            else
            {
                _panelsHistory.RemoveAt(_panelsHistory.Count-1);

                SetActivePanel(_panelsHistory[^1],true);
            }
        }
    }
}