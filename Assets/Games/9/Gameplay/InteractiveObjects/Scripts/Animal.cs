using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game9.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Animal : BaseInteractiveObject
    {
        private const float _timeMoveToCenter = 2f;
        private const float _timeChangeColor = 1.5f;
        private const float _speedMoveToCenter = 1f;
        private const float _maxScaleInCenter = 2.5f;

        [SerializeField] private bool _randomFlip;

        private SpriteRenderer _spriteRenderer;
        private bool _isMayDestroyed;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.flipX = _randomFlip ? Random.Range(0, 2) != 0 : _spriteRenderer.flipX;
        }

        private void OnDisable()
        {
            if (_isMayDestroyed)
                Destroy(gameObject);
        }

        public override void ShowHint() { }

        public override void Found()
        {
            StartCoroutine(MoveToCenterRoutine());
            enabled = true;
        }

        private IEnumerator MoveToCenterRoutine()
        {
            _isMayDestroyed = true;
            
            _spriteRenderer.sortingOrder = 120;
            
            float time = 0f;
            float startScale = transform.localScale.x;
            Vector3 endPoint = new Vector3(0f, -_spriteRenderer.bounds.size.y * 2);
            while (time < _timeMoveToCenter)
            {
                time += Time.deltaTime;
                transform.position =
                    Vector3.Lerp(transform.position, endPoint,Time.deltaTime * _speedMoveToCenter);

                float scale = time / _timeMoveToCenter * _maxScaleInCenter + startScale;
                transform.localScale = new Vector3(scale, scale, 1f);

                yield return null;
            }

            time = 0f;
            Color color = _spriteRenderer.color;
            while (time < _timeChangeColor)
            {
                color.a = 1f - time / _timeChangeColor;
                _spriteRenderer.color = color;
                
                time += Time.deltaTime;
                yield return null;
            }
            
            Destroy(gameObject);
            _isMayDestroyed = false;
        }
    }
}
