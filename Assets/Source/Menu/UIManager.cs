using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Menu
{
    public enum UIScreenType
    {
        MainMenu,
        LoadSave,
        Settings,
        Credits
    }

    public class UIManager : MonoBehaviour
    {
        [Serializable]
        public struct ScreenReference
        {
            public UIScreenType type;
            public BaseUIScreen screen;
        }
        
        [SerializeField] private ScreenReference[] _screens;
        [SerializeField] private GameObject _hintCanvas;
        
        private Dictionary<UIScreenType, BaseUIScreen> _screenMap;
        private BaseUIScreen _currentScreen;
        private BaseUIScreen _previousScreen;
        
        private void Awake()
        {
            _screenMap = new Dictionary<UIScreenType, BaseUIScreen>();
            foreach (var entry in _screens)
            {
                _screenMap[entry.type] = entry.screen;
                entry.screen.gameObject.SetActive(false);
            }
            
            _hintCanvas.SetActive(false);
        }

        public void ShowHintCanvas()
        {
            _hintCanvas.SetActive(true);
        }
        
        public void ShowScreen(UIScreenType type)
        {
            if (_screenMap.TryGetValue(type, out var nextScreen))
            {
                if (_currentScreen != null)
                {
                    _currentScreen.StartFadeOut(() =>
                    {
                        _previousScreen = _currentScreen;
                        _currentScreen = nextScreen;
                        _currentScreen.gameObject.SetActive(true);
                    });
                }
                else
                {
                    _currentScreen = nextScreen;
                    _currentScreen.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning($"UIManager: No screen found for type {type}");
            }
        }

        public void ShowPreviousScreen()
        {
            if (_previousScreen != null)
            {
                foreach (var pair in _screenMap.Where(pair => pair.Value == _previousScreen))
                {
                    ShowScreenWithoutUpdatingHistory(pair.Key);
                    return;
                }
            }
            else
            {
                Debug.LogWarning("UIManager: No previous screen to return to.");
            }
        }

        private void ShowScreenWithoutUpdatingHistory(UIScreenType type)
        {
            if (_screenMap.TryGetValue(type, out var nextScreen))
            {
                if (_currentScreen != null)
                {
                    _currentScreen.StartFadeOut(() =>
                    {
                        _currentScreen = nextScreen;
                        _currentScreen.gameObject.SetActive(true);
                    });
                }
                else
                {
                    _currentScreen = nextScreen;
                    _currentScreen.gameObject.SetActive(true);
                }
            }
        }
        
        public BaseUIScreen GetScreen(UIScreenType type) =>
            _screenMap.TryGetValue(type, out var screen) ? screen : null;
    }
}