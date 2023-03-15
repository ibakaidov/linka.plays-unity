using System;
using System.Collections;
using System.Collections.Generic;
using Game9.Gameplay;
using Shell;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game9.Levels
{
    [System.Serializable]
    public class ObstaclesHandler
    {
        public event Action ClearedAll;
        
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private float _tickHint = 5f;
        [SerializeField] private AudioClip[] _clipsFromDestroyObstacle;
        private List<Obstacle> _obstacles = new List<Obstacle>();

        private Coroutine _coroutine;
        private float _time;

        public void Start(List<Obstacle> obstacles)
        {
            _obstacles.AddRange(obstacles);
            _coroutine = _levelGenerator.StartCoroutine(TickHintRoutine());
            UIMediator.InitCounterObjects(_obstacles.Count);

            for (var i = 0; i < _obstacles.Count; ++i)
                _obstacles[i].Destroyed += OnDestroyed;
        }

        public void Stop()
        {
            for (var i = 0; i < _obstacles.Count; ++i)
                _obstacles[i].Destroyed -= OnDestroyed;
            _obstacles.Clear();

            if (_coroutine != null)
            {
                _levelGenerator.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator TickHintRoutine()
        {
            _time = 0f;
            while (true)
            {
                while (_time < _tickHint)
                {
                    _time += Time.deltaTime;
                    yield return null;
                }

                _obstacles[Random.Range(0, _obstacles.Count)].ShowHint();
                _time = 0f;
            }
        }

        private void OnDestroyed(Obstacle obstacle)
        {
            obstacle.Destroyed -= OnDestroyed;
            UIMediator.DecrementCounterObjects();;
            
            Object.Destroy(obstacle.gameObject);
            _obstacles.Remove(obstacle);
            _time = 0f;
            
            SoundsHandler.PlayOneShot(_clipsFromDestroyObstacle[Random.Range(0, _clipsFromDestroyObstacle.Length)], 1f);

            if (_obstacles.Count == 0)
                ClearedAll?.Invoke();
        }
    }
}
