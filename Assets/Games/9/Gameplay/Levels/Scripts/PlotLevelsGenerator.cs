using System.Collections.Generic;
using Game9.Gameplay;
using UnityEngine;

namespace Game9.Levels
{
    [System.Serializable]
    public class DifficultyData
    {
        [SerializeField] private string _name;
        [SerializeField] private PlotLevelData[] _datas;

        public IReadOnlyList<PlotLevelData> Datas => _datas;
        public string Name => _name;
    }
    
    [System.Serializable]
    public class PlotLevelData
    {
        [SerializeField] private LevelPlotContainer _prefab;
        [SerializeField] private Sprite _icon;

        public LevelPlotContainer Prefab => _prefab;

        public Sprite Icon => _icon;
    }
    
    [System.Serializable]
    public class PlotLevelsGenerator : ILevelGenerator
    {
        [SerializeField] private DifficultyData[] _difficulties;
        private LevelGenerator _levelGenerator;

        private int _currentIndexLevel;
        private int _currentDifficulty;

        public void Init(LevelGenerator levelGenerator)
        {
            _levelGenerator = levelGenerator;
            
            UIMediator.InitLevelsContainer(_difficulties);
            UIMediator.ChoicedLevel += OnChoicedLevel;
            UIMediator.ChoicedDifficulty += OnChoicedDifficulty;
        }

        private void OnChoicedDifficulty(int index)
        {
            _currentDifficulty = index;
        }

        private void OnChoicedLevel(int indexLevel)
        {
            _currentIndexLevel = indexLevel;
            _levelGenerator.Generate();
        }

        public List<Obstacle> Generate()
        {
            var obstacles = Object.Instantiate(_difficulties[_currentDifficulty].Datas[_currentIndexLevel].Prefab, _levelGenerator.transform).Obstacles;

            for (int i = 0; i < obstacles.Count; i++)
                obstacles[i].Init(obstacles[i].GetComponentInChildren<BaseInteractiveObject>());

            return obstacles;
        }

        public void PrepareForNextGeneration()
        {
            ++_currentIndexLevel;
            if (_currentIndexLevel == _difficulties[_currentDifficulty].Datas.Count)
            {
                _currentIndexLevel = 0;
                ++_currentDifficulty;
                if (_currentDifficulty == _difficulties.Length)
                    _currentDifficulty = 0;
            }
        }
    }
}
