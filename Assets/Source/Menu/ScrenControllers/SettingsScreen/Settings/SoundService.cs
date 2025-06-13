using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Menu
{
    public class SoundService : MonoBehaviour
    {
        [Serializable]
        public struct VolumeData
        {
            public SoundType type;
            public string parameter;
        }

        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private VolumeData[] _volumeData;

        private Dictionary<SoundType, string> _soundTypes = new();

        protected void Awake()
        {
            for (int i = 0; i < _volumeData.Length; ++i)
                _soundTypes.Add(_volumeData[i].type, _volumeData[i].parameter);
        }

        public void SetVolume(SoundType type, float volume)
        {
            _audioMixer.SetFloat(_soundTypes[type], VolumeToDecibels(volume));
        }

        public float GetVolume(SoundType type)
        {
            float volume;
            _audioMixer.GetFloat(_soundTypes[type], out volume);
            return DecibelsToVolume(volume);
        }
        
        private static float VolumeToDecibels(float volume)
        {
            return volume > 0 ? 20f * Mathf.Log10(volume) : -80f;
        }

        private static float DecibelsToVolume(float dB)
        {
            return Mathf.Pow(10f, dB / 20f);
        }
    }
}