using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game6.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class MovingOutWindow : MonoBehaviour
    {
        [SerializeField] private float _timeOpened;
        [SerializeField] private EasingCurve _curve;
        [SerializeField] private Button _button;
        [SerializeField] private Vector2 _directionChange;
        [SerializeField] private bool _defaultState;

        [SerializeField] private RectOffset _padding;

        private RectTransform _rectTransform;
        private bool _isActive;
        private float _currentTime;
        private Coroutine _routine;
        private Vector2 _sizeDelta;
        private Vector2 _offsetPosition;
        
        #if UNITY_EDITOR
        private bool? _lastState;
        #endif

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _sizeDelta = _rectTransform.sizeDelta - new Vector2(_padding.left + _padding.right, _padding.top + _padding.bottom);
            _offsetPosition = _defaultState ? _rectTransform.anchoredPosition : _rectTransform.anchoredPosition - _sizeDelta * _directionChange;
            
            _button.onClick.AddListener(OnClicked);

            _isActive = _defaultState;
            if (_isActive)
            {
                _rectTransform.anchoredPosition = _offsetPosition;
            }
            else
            {
                _currentTime = _timeOpened;
                _rectTransform.anchoredPosition = _sizeDelta * _directionChange + _offsetPosition;
            }
        }

        private void OnClicked()
        {
            _isActive = !_isActive;

            if (_routine != null)
                StopCoroutine(_routine);

            _routine = StartCoroutine(_isActive ?
                MoveRoutine(time => time > 0, -1, Vector2.zero, 0) :
                MoveRoutine(time => time < _timeOpened, 1, _sizeDelta * _directionChange, _timeOpened));
        }

        private IEnumerator MoveRoutine(Predicate<float> predicate, int directionChange, Vector2 endXPosition, float endTime)
        {
            while (predicate(_currentTime))
            {
                _rectTransform.anchoredPosition = _curve.Evaluate(_currentTime / _timeOpened) *
                    _sizeDelta * _directionChange + _offsetPosition;
                _currentTime += Time.deltaTime * directionChange;
                yield return null;
            }

            _rectTransform.anchoredPosition = endXPosition + _offsetPosition;
            _currentTime = endTime;
            _routine = null;
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (_lastState.HasValue == false)
            {
                _lastState = _defaultState;
                return;
            }
            
            if (_lastState == _defaultState)
                return;

            RectTransform rectTransform = GetComponent<RectTransform>();
            Vector2 sizeDelta = rectTransform.sizeDelta - new Vector2(_padding.left + _padding.right, _padding.top + _padding.bottom);
            rectTransform.anchoredPosition += sizeDelta * _directionChange * (_defaultState ? -1f : 1f);

            _lastState = _defaultState;
        }
        #endif
    }
}
