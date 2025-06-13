using System;
using System.Collections;
using Input;
using UnityEngine;

namespace Menu
{
    public abstract class BaseUIScreen : MonoBehaviour
    {
        [Header("Fade Settings")]
        [SerializeField] protected float _fadeDuration = 2.0f;
        [SerializeField] protected CanvasGroup _canvasGroup;
        
        [Header("Dependencies")]
        [SerializeField] protected UIManager _uiManager;
        [SerializeField] protected InputDeviceDetector _inputDeviceDetector;

        protected virtual void Awake()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(FadeInRoutine());
        }

        public void StartFadeOut(Action onComplete = null)
        {
            StartCoroutine(FadeOutRoutine(onComplete));
        }

        protected virtual IEnumerator FadeInRoutine()
        {
            float time = 0f;
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                float t = time / _fadeDuration;
                _canvasGroup.alpha = t;
                yield return null;
            }

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            OnFadeInComplete();
        }

        protected virtual IEnumerator FadeOutRoutine(Action onComplete = null)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            float time = 0f;
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                float t = time / _fadeDuration;
                _canvasGroup.alpha = 1f - t;
                yield return null;
            }

            _canvasGroup.alpha = 0f;
            onComplete?.Invoke();
            OnFadeOutComplete();
            gameObject.SetActive(false);
        }

        protected virtual void OnFadeInComplete() { }
        protected virtual void OnFadeOutComplete() { }
    }
}