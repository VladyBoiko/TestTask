using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class CreditsScreenController : BaseUIScreen
    {
        [Header("Credits Elements")]
        [SerializeField] private Button _backButton;

        protected override void OnFadeInComplete()
        {
            _backButton.onClick.AddListener(() => 
                _uiManager.ShowScreen(UIScreenType.MainMenu));
            
            if (_inputDeviceDetector.CurrentDeviceType != "Keyboard")
            {
                EventSystem.current.SetSelectedGameObject(_backButton.gameObject);
            }
        }

        protected override void OnFadeOutComplete()
        {
            _backButton.onClick.RemoveAllListeners();
        }
        
    }
}