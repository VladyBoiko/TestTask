using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace Menu
{
    public class TitleScreenController : BaseUIScreen
    {
        [Header("Black Background")]
        [SerializeField] private Image _blackBackground;

        [Header("Blinking Text")]
        [SerializeField] private Graphic _pressAnyButtonText;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _buttonClip;

        [Header("Blink Settings")]
        [SerializeField] private float _blinkSpeed = 1.0f;

        private IDisposable _mEventListener;
        private bool _inputEnabled = false;
        private Color _originalTextColor;

        protected override void Awake()
        {
            base.Awake();
            _originalTextColor = _pressAnyButtonText.color;
            _pressAnyButtonText.color = SetAlpha(_originalTextColor, 0f);

            _blackBackground.color = Color.black;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _mEventListener = InputSystem.onAnyButtonPress.Call(HandleAnyButtonPress);
        }

        private void OnDisable()
        {
            _mEventListener?.Dispose();
        }

        private void Update()
        {
            if (!_inputEnabled) return;

            float blink = Mathf.Sin(Time.time * _blinkSpeed) * 0.5f + 0.5f;
            _pressAnyButtonText.color = SetAlpha(_originalTextColor, blink);
        }

        protected override IEnumerator FadeInRoutine()
        {
            float time = 0f;
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                float t = time / _fadeDuration;
                _canvasGroup.alpha = t;
                _blackBackground.color = SetAlpha(Color.black, 1f - t);
                yield return null;
            }

            _canvasGroup.alpha = 1f;
            _blackBackground.color = Color.clear;

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnFadeInComplete();
        }

        protected override void OnFadeInComplete()
        {
            _inputEnabled = true;
        }

        private void HandleAnyButtonPress(InputControl control)
        {
            if (!_inputEnabled) return;

            _inputEnabled = false;
            _audioSource?.PlayOneShot(_buttonClip);

            StartFadeOut(() =>
            {
                _uiManager.ShowScreen(UIScreenType.MainMenu);
            });
        }
        
        private static Color SetAlpha(Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}
