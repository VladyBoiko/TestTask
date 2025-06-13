using UnityEngine;

namespace Menu
{
    public class CreditsAutoScroll : MonoBehaviour
    {
        [SerializeField] private RectTransform _textTransform;
        [SerializeField] private float _scrollSpeed = 50f;
        
        private float _startY;

        private void Awake()
        {
            _startY = _textTransform.anchoredPosition.y;
        }
        
        private void OnEnable()
        {
            _textTransform.anchoredPosition = new Vector2(_textTransform.anchoredPosition.x, _startY);
        }

        private void Update()
        {
            _textTransform.anchoredPosition += Vector2.up * (_scrollSpeed * Time.unscaledDeltaTime);
        }
    }
}