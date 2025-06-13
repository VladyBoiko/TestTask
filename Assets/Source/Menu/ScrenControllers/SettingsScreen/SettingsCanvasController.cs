using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsCanvasController : BaseUIScreen
    {
        [Header("Settings Ellements")]
        [SerializeField] private List<SettingsTabEntry> _tabs;
        [SerializeField] private Button _backButton;
        
        private Dictionary<SettingsTabType, GameObject> _panels = new();
        private SettingsTabType _currentTab;

        protected override void OnFadeInComplete()
        {
            if (_inputDeviceDetector.CurrentDeviceType != "Keyboard")
            {
                EventSystem.current.SetSelectedGameObject(_tabs[0].TabButton.gameObject);
            }
        }
        
        protected override void Awake()
        {
            foreach (var tab in _tabs)
            {
                _panels[tab.Type] = tab.Panel;
                
                var tabType = tab.Type;
                tab.TabButton.onClick.AddListener(() => ShowPanel(tabType));
            }

            _backButton.onClick.AddListener(() => _uiManager.ShowPreviousScreen());

            if (_tabs.Count > 0)
                ShowPanel(_tabs[0].Type); 
        }

        private void ShowPanel(SettingsTabType type)
        {
            foreach (var panel in _panels.Values)
                panel.SetActive(false);

            _panels[type].SetActive(true);
            _currentTab = type;
        }
    }
}