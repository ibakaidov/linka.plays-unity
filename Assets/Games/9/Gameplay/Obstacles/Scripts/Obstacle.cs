using System;
using UnityEngine;
using Shell;
using Random = UnityEngine.Random;

namespace Game9.Gameplay
{
    [RequireComponent(typeof(IEyeTrackerButton), typeof(SpriteRenderer))]
    public class Obstacle : MonoBehaviour, IEyeUpdater
    {
        public event Action<Obstacle> Destroyed;

        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private Animation _animation;
        private BaseInteractiveObject _interactiveObject;
        private IEyeTrackerButton _eyeTrackerButton;

        private SpriteRenderer _spriteRenderer;
        
        public bool IsValid { get; private set; } = true;

        public void Init(BaseInteractiveObject interactiveObject)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            _interactiveObject = interactiveObject;
            _interactiveObject.transform.SetParent(transform, false);

            _eyeTrackerButton = GetComponent<IEyeTrackerButton>();
            _eyeTrackerButton.Begun += OnBegunEyeButton;
            _eyeTrackerButton.Ended += OnEndedEyeButton;
        }

        private void OnDestroy() => IsValid = false;

        public void RandomFlipSprite() => _spriteRenderer.flipX = Random.Range(0, 2) != 0;

        public void ShowHint()
        {
            if (_particles)
                _particles.Play();
            if (_animation)
                _animation.Play();
                
            _interactiveObject.ShowHint();
        }

        private void OnBegunEyeButton()
        {
            Vector3 position = transform.position + Vector3.up * (_spriteRenderer.bounds.size.y / 2);
            UIMediator.RegisterEyeButton(this, position);
        }
        
        private void OnEndedEyeButton() => UIMediator.UnregisterEyeButton(this);

        public void UpdateEyeRation(float ratio) { }
        
        public void ClickFromEye()
        {
            Destroyed?.Invoke(this);
            _interactiveObject.transform.SetParent(transform.parent);
            _interactiveObject.Found();
            OnEndedEyeButton();
        }
    }
}
