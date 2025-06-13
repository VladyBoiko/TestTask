using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class LoadSaveScreenController : BaseUIScreen
    {
        [Header("Left Panel")]
        [SerializeField] private Transform _saveListContainer;
        [SerializeField] private SaveSlotUI _saveSlotPrefab;

        [Header("Right Panel")]
        [SerializeField] private GameObject _rightPanel;
        [SerializeField] private Image _previewImage;
        [SerializeField] private TMP_Text _saveNameText;
        [SerializeField] private TMP_Text _saveDateText;
        [SerializeField] private TMP_Text _saveDescriptionText;

        [Header("Buttons")]
        [SerializeField] private Button _newSaveButton;
        [SerializeField] private Button _deleteSaveButton;
        [SerializeField] private Button _backButton;

        private List<SaveData> _saves = new();
        private SaveData _selectedSave;

        private byte _saveSlotsCount = 0;
        
        protected override void Awake()
        {
            LoadMockSaves();
            InitRightPanel();
        }
        
        protected override void OnFadeInComplete()
        {
            _newSaveButton.onClick.AddListener(CreateNewSave);
            _deleteSaveButton.onClick.AddListener(DeleteSelectedSave);
            _backButton.onClick.AddListener(() => 
                _uiManager.ShowScreen(UIScreenType.MainMenu));
            
            if (_inputDeviceDetector.CurrentDeviceType != "Keyboard")
            {
                EventSystem.current.SetSelectedGameObject(_newSaveButton.gameObject);
            }
        }

        protected override void OnFadeOutComplete()
        {
            _newSaveButton.onClick.RemoveAllListeners();
            _deleteSaveButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }

        private void LoadMockSaves()
        {
            _saves.Clear();
            foreach (Transform child in _saveListContainer)
                Destroy(child.gameObject);
            
            // TODO: Replace with actual save loading logic
            
            for (int i = 1; i <= 3; i++)
            {
                var data = new SaveData
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"Save {i}",
                    Date = DateTime.Now.AddMinutes(-i * 15),
                    Description = "Sample description for save " + i,
                    PreviewSprite = null
                };
                
                _saveSlotsCount++;
                _saves.Add(data);
                AddSaveSlot(data);
            }
        }

        private void AddSaveSlot(SaveData data)
        {
            var slot = Instantiate(_saveSlotPrefab, _saveListContainer);
            slot.Initialize(data, () => OnSaveSelected(data));
        }

        private void OnSaveSelected(SaveData data)
        {
            _selectedSave = data;
            _previewImage.sprite = data.PreviewSprite;
            _previewImage.enabled = data.PreviewSprite;
            // _previewImage.enabled = true;
            
            _saveNameText.text = data.Name;
            _saveDateText.text = data.Date.ToString("g");
            _saveDescriptionText.text = data.Description;
            
        }

        private void CreateNewSave()
        {
            if(_saves.Count >= 6)
            {
                Debug.LogWarning("Maximum save slots reached. Cannot create new save.");
                return;
            }
            
            var newSave = new SaveData
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New Save " + (_saveSlotsCount + 1),
                Date = DateTime.Now,
                Description = "Created manually.",
                PreviewSprite = null
            };

            _saveSlotsCount++;
            _saves.Add(newSave);
            AddSaveSlot(newSave);
            OnSaveSelected(newSave);
        }

        private void DeleteSelectedSave()
        {
            if (_selectedSave == null) return;

            _saves.Remove(_selectedSave);

            foreach (Transform child in _saveListContainer)
            {
                var slot = child.GetComponent<SaveSlotUI>();
                if (slot != null && slot.SaveId == _selectedSave.Id)
                {
                    Destroy(slot.gameObject);
                    break;
                }
            }

            _selectedSave = null;
            InitRightPanel();
        }

        private void InitRightPanel()
        {
            _previewImage.sprite = null;
            _previewImage.enabled = false;
            
            _saveNameText.text = "...";
            _saveDateText.text = "...";
            _saveDescriptionText.text = "...";
        }
    }
}
