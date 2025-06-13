using Audio;
using Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu
{
    public class UIButtonEffects : UISelectableScalerBase, 
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        ISelectHandler, IDeselectHandler, ISubmitHandler
    {
        [Header("Color settings")]
        [SerializeField] private Graphic _graphic;
        [SerializeField] private Color _hoverColor = Color.yellow;
        [SerializeField] private Color _clickColor = Color.green;
        [SerializeField] private float _colorSpeed = 8f;
        private Color _originalColor;
        private Color _targetColor;

        [Header("Audio settings")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _hoverSound;
        [SerializeField] private AudioClip _clickSound;
    
        private bool _isPointerOver;
        
        protected override void Awake()
        {
            base.Awake();

            if (_graphic == null) return;
            _originalColor = _graphic.color;
            _targetColor = _originalColor;
        }

        private void Start()
        {
            // TODO: Better to get from ServiceLocator, DI or similar to avoid direct dependency on AudioManager
            if (!_audioSource)
                _audioSource = AudioManager.Instance.UIAudioSource;
        }

        protected override void Update()
        {
            base.Update();
        
            if (!_graphic) return;
            _graphic.color = Color.Lerp(_graphic.color, _targetColor, Time.unscaledDeltaTime * _colorSpeed);
        }
    
        private void PlaySound(AudioClip clip)
        {
            if (_audioSource && clip)
                _audioSource.PlayOneShot(clip);
        }

        protected override void SetHoverState()
        {
            base.SetHoverState();
            _targetColor = _hoverColor;
            PlaySound(_hoverSound);
        }

        protected override void SetOriginalState()
        {
            base.SetOriginalState();
            _targetColor = _originalColor;
        }

        private void SetClickState()
        {
            _targetColor = _clickColor;
            PlaySound(_clickSound);
            
            StartCoroutine(ResetAfterClick());
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerOver = true;
            SetHoverState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerOver = false;
            SetOriginalState();
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            if (!_isPointerOver)
                SetHoverState();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (!_isPointerOver)
                SetOriginalState();
        }
        
        public void OnPointerClick(PointerEventData eventData) => SetClickState();
        
        public void OnSubmit(BaseEventData eventData) => SetClickState();
        
        private System.Collections.IEnumerator ResetAfterClick()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            SetOriginalState();
        }
    }
}