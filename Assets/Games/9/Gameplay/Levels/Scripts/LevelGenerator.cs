using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game9.Levels
{
    public enum LevelHandlerType
    {
        Plot,
        Random,
    }
    
    public class LevelGenerator : MonoBehaviour
    {
        private Dictionary<LevelHandlerType, ILevelGenerator> _levelHandlers;

        [SerializeField] private ParticleSystem _transitionToAnotherLevel;
        [SerializeField] private LevelHandlerType _startLevelHandlerType;
        [SerializeField] private RandomGenerator _randomGenerator;
        [SerializeField] private PlotLevelsGenerator _plotLevelsGenerator;
        [SerializeField] private ObstaclesHandler _obstaclesHandler;
        [SerializeField] private float _waitTimeAfterEndLevel = 5f;
        
        private ILevelGenerator _currentGenerator;

        private bool _isInited;
        
        private void OnEnable()
        {
            if (_isInited == false)
            {
                Init();
                _isInited = true;
            }
            
            UIMediator.ChoicedLevelGeneratorType += OnChooseLevelGeneratorType;
            Generate();
        }

        private void OnDisable()
        {
            UIMediator.ChoicedLevelGeneratorType -= OnChooseLevelGeneratorType;
            ClearChilds();
        }
        
        private void OnDestroy()
        {
            _obstaclesHandler.ClearedAll -= OnClearedAllObstacles;
            _obstaclesHandler.Stop();
        }

        public void Generate()
        {
            _obstaclesHandler.Stop();
            ClearChilds();

            var obstacles = _currentGenerator.Generate();
            
            UIMediator.ResetTimer();
            UIMediator.ContinueTimer();

            if (obstacles.Count != 0)
                _obstaclesHandler.Start(obstacles);
        }
        
        private void Init()
        {
            _levelHandlers = new Dictionary<LevelHandlerType, ILevelGenerator>()
            {
                [LevelHandlerType.Random] = _randomGenerator,
                [LevelHandlerType.Plot] = _plotLevelsGenerator
            };
            
            foreach (var handler in _levelHandlers.Values)
                handler.Init(this);

            _currentGenerator = _levelHandlers[_startLevelHandlerType];
            _obstaclesHandler.ClearedAll += OnClearedAllObstacles;
        }

        private void ClearChilds()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }

        private void OnClearedAllObstacles()
        {
            UIMediator.StopTimer();
            StartCoroutine(WaitEndLevelRoutine());
            _obstaclesHandler.Stop();
        }
        
        private void OnChooseLevelGeneratorType(int lastIndex, int currentIndex)
        {
            _currentGenerator = _levelHandlers[(LevelHandlerType)currentIndex];
            Generate();
        }

        private IEnumerator WaitEndLevelRoutine()
        {
            const float timeToPlayTransition = 1f;
            const float waitTimeForNewGeneration = 2f;

            yield return new WaitForSeconds(timeToPlayTransition);
            
            _transitionToAnotherLevel.Play();
            
            yield return new WaitForSeconds(waitTimeForNewGeneration);
            
            _currentGenerator.PrepareForNextGeneration();
            Generate();
            
            yield return new WaitForSeconds(_waitTimeAfterEndLevel - timeToPlayTransition - waitTimeForNewGeneration);
        }
    }
}
