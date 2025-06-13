using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class AudioSettings : MonoBehaviour
    {
        [Serializable]
        public struct SoundSlider
        {
            public SoundType type;
            public Slider slider;
        }
        
        [SerializeField] private SoundSlider[] _sliders;
        [SerializeField] private SoundService _soundService;
        
        private void Start()
        {
            for (int i = 0; i < _sliders.Length; ++i)
            {
                SoundType soundType = _sliders[i].type;
                Slider slider = _sliders[i].slider;
                slider.SetValueWithoutNotify(_soundService.GetVolume(soundType));
                slider.onValueChanged.AddListener((value) => SliderHandler(soundType, value));
            }
        }
        
        private void SliderHandler(SoundType soundType, float value)
        {
            _soundService.SetVolume(soundType, value);
        }
    }
}