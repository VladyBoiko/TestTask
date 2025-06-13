using System;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SaveSlotUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _saveNameText;
        [SerializeField] private Button _selectButton;

        public string SaveId { get; private set; }

        private float _lastClickTime;
        private const float DoubleClickThreshold = 0.3f;

        public void Initialize(SaveData data, Action onSelected)
        {
            SaveId = data.Id;
            _saveNameText.text = data.Name;

            _selectButton.onClick.AddListener(() =>
            {
                if (Time.time - _lastClickTime < DoubleClickThreshold)
                {
                    // TODO: LoadSave(data.Id);
                    Debug.Log("Double click: Load save " + data.Name);
                }
                else
                {
                    onSelected?.Invoke();
                }
                _lastClickTime = Time.time;
            });
        }
    }
}