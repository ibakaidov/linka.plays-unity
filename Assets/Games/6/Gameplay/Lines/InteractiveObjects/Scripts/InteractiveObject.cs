using System;
using System.Collections;
using Shell;
using UnityEngine;

namespace Game6
{
    [RequireComponent(typeof(EyeTrackerButton), typeof(Rigidbody2D))]
    public class InteractiveObject : MonoBehaviour, IInteractiveObject, IEyeUpdater
    {
        public event Action Destroyed;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private GameObject _vfxFromDeath;
        
        private IEyeTrackerButton _eyeTrackerButton;
        private Rigidbody2D _rigidbody;
        private Vector2 _currentEndPosition;
        private float _speedToEndPosition;

        private Vector3 CenterPosition => transform.position + new Vector3(0f, _renderer.bounds.size.y / 2f);
        public bool IsValid { get; private set; } = true;

        public void MoveTo(Vector2 position, float speed)
        {
            _currentEndPosition = position;
            _speedToEndPosition = speed;
        }

        public void ChangeDirection(int direction) => _renderer.flipX = direction == -1;
        public void ChangeSprite(Sprite sprite) => _renderer.sprite = sprite;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _eyeTrackerButton = GetComponent<IEyeTrackerButton>();
            _eyeTrackerButton.Begun += OnBegunEyeTrackerButton;
            _eyeTrackerButton.Ended += OnEndedEyeTrackerButton;
        }

        private void OnDestroy() => IsValid = false;

        private void FixedUpdate()
        {
            MoveToEndPosition(_currentEndPosition, _speedToEndPosition);
        }

        public void UpdateEyeRation(float ratio)
        {
            UIMediator.ChangeTrackerRingPosition(CenterPosition);
            _renderer.color = Color.Lerp(Color.white, Color.red, ratio);
        }

        public void ClickFromEye()
        {
            Destroyed?.Invoke();
            if (_vfxFromDeath)
            {
                var vfx = Instantiate(_vfxFromDeath, transform.position, Quaternion.identity, transform.parent);
                vfx.transform.localScale = transform.lossyScale;
            }
                
            OnEndedEyeTrackerButton();
        }

        private void MoveToEndPosition(Vector2 position, float speed)
        {
            if ((_rigidbody.position - position).sqrMagnitude <= 0.02f)
                return;

            _rigidbody.MovePosition(Vector2.Lerp(transform.position, position, speed * Time.fixedDeltaTime));
        }
        
        private void OnBegunEyeTrackerButton()
        {
            UIMediator.RegisterTrackerButton(this, CenterPosition);
        }

        private void OnEndedEyeTrackerButton()
        {
            UIMediator.UnregisterTrackerButton(this);
            _renderer.color = Color.white;
        }
    }
}
