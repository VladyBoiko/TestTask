using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenuController : BaseUIScreen
    {
        [Header("MainMenu Elements")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _creditsButton;
        [SerializeField] private Button _exitButton;

        protected override void OnFadeInComplete()
        {
            _uiManager.ShowHintCanvas();
            
            _startButton.onClick.AddListener(OnStartClicked);
            _loadButton.onClick.AddListener(OnLoadClicked);
            _settingsButton.onClick.AddListener(OnSettingsClicked);
            _creditsButton.onClick.AddListener(OnCreditsClicked);
            _exitButton.onClick.AddListener(OnExitClicked);
            
            if (_inputDeviceDetector.CurrentDeviceType != "Keyboard")
            {
                EventSystem.current.SetSelectedGameObject(_startButton.gameObject);
            }
        }

        protected override void OnFadeOutComplete()
        {
            _startButton.onClick.RemoveAllListeners();
            _loadButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _creditsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void OnStartClicked()
        {
            // Завантаження gameplay сцени
            Debug.Log("Start Game clicked");
        }

        private void OnLoadClicked()
        {
            LoadScene(UIScreenType.LoadSave);
        }

        private void OnSettingsClicked()
        {
            LoadScene(UIScreenType.Settings);
        }

        private void OnCreditsClicked()
        {
            LoadScene(UIScreenType.Credits);
        }

        private void LoadScene(UIScreenType screenType)
        {
            _uiManager.ShowScreen(screenType);
        }
        
        private void OnExitClicked()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}