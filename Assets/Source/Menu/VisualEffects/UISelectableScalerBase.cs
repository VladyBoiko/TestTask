using UnityEngine;

namespace Menu
{
    public abstract class UISelectableScalerBase : MonoBehaviour
    {
        [SerializeField] protected RectTransform _rectTransform;
        [SerializeField] protected Vector3 _hoverScale = new(1.1f, 1.1f, 1f);
        [SerializeField] protected float _scaleSpeed = 8f;

        protected Vector3 _originalScale;
        protected Vector3 _targetScale;

        protected virtual void Awake()
        {
            _originalScale = _rectTransform.localScale;
            _targetScale = _originalScale;
        }

        protected virtual void Update()
        {
            _rectTransform.localScale = Vector3.Lerp(
                _rectTransform.localScale,
                _targetScale,
                Time.unscaledDeltaTime * _scaleSpeed
            );
        }

        protected virtual void SetHoverState()
        {
            _targetScale = _hoverScale;
        }

        protected virtual void SetOriginalState()
        {
            _targetScale = _originalScale;
        }
    }
}