using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game6
{
    public class SpawnerInteractiveObjects : MonoBehaviour
    {
        public event Action DestroyedObject;
        
        private const float _minXRandom = 10;
        private const float _maxXRandom = 15;
        
        [SerializeField] private int _direction;
        
        private LineData _lineData;

        public void SetData(LineData lineData) => _lineData = lineData;

        private InteractiveObject _currentInteractiveObject;
        private Vector3 _currentScale;
        
        public void TurnOn() => Spawn();

        public void TurnOff()
        {
            if (_currentInteractiveObject != null)
                Destroy(_currentInteractiveObject.gameObject);
        }

        public void ChangeScale(float scale)
        {
            _currentScale = new Vector3(scale, scale, 1f);
            if (_currentInteractiveObject != null)
                _currentInteractiveObject.transform.localScale = _currentScale;
        }
        
        private void Spawn()
        {
            var interactiveObject = Instantiate(_lineData.InteractiveObject, transform);
            interactiveObject.ChangeDirection(_direction);
            interactiveObject.ChangeSprite(_lineData.Sprites[Random.Range(0, _lineData.Sprites.Count)]);
            interactiveObject.transform.localScale = _currentScale;
            interactiveObject.Destroyed += OnDestroyedOnInteractiveObject;

            Vector3 position = transform.position;
            position.x += Random.Range(_minXRandom, _maxXRandom) * _direction;
            interactiveObject.MoveTo(position, _lineData.Speed);

            _currentInteractiveObject = interactiveObject;
        }

        private void OnDestroyedOnInteractiveObject()
        {
            DestroyedObject?.Invoke();
            Destroy(_currentInteractiveObject.gameObject);
            Spawn();
        }
    }
}
