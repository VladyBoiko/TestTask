using UnityEngine;

namespace Background
{
    public class CloudLooper : MonoBehaviour
    {
        [SerializeField] private float _resetDistance = 50f;
        [SerializeField] private float _speed = 0.5f;
    
        private Vector3 _startPosition;

        private void Start() {
            _startPosition = transform.position;
        }

        private void Update()
        {
            transform.position += Vector3.forward * (Time.deltaTime * _speed);

            if (Vector3.Distance(_startPosition, transform.position) > _resetDistance)
            {
                transform.position = _startPosition;
            }
        }
    }
}
