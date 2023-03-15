using System.Collections.Generic;
using System.Linq;
using Game9.Gameplay;
using UnityEngine;

namespace Game9.Levels
{
    public class RemainsList<T>
    {
        private readonly IList<T> _original;
        private List<T> _remains;

        public T this[int index] => _remains[index];

        public RemainsList(IList<T> original)
        {
            _original = original;
            _remains = new List<T>(original);
        }
        
        public void RemoveAt(int index)
        {
            _remains.RemoveAt(index);
            
            if (_remains.Count == 0)
                _remains = new List<T>(_original);
        }
        
        public T GetRandomElementAndRemove()
        {
            int randomIndex = Random.Range(0, _remains.Count);
            var element = this[randomIndex];
            RemoveAt(randomIndex);
            return element;
        }
    }
    
    [System.Serializable]
    public class RandomGenerator : ILevelGenerator
    {
        private class ObjectCountInGroup
        {
            public readonly ObstacleGroupInfo Info;
            public int Count;
            
            public ObjectCountInGroup(ObstacleGroupInfo info, int count)
            {
                Info = info;
                Count = count;
            }
        }
        
        private const float _offsetCoefficientFromXBetweenObjects = 0.2f;

        [SerializeField] private RandomLevelData[] _levelDatas;
        [SerializeField] private int _count = 5;

        private LevelGenerator _levelGenerator;
        private RemainsList<RandomLevelData> _remainsLevels;
        private RandomLevelData _currentData;

        public void Init(LevelGenerator levelGenerator)
        {
            _remainsLevels = new RemainsList<RandomLevelData>(_levelDatas);
            _levelGenerator = levelGenerator;
        }
        
        public List<Obstacle> Generate()
        {
            var levelData = _remainsLevels.GetRandomElementAndRemove();

            Object.Instantiate(levelData.Background, _levelGenerator.transform);

            var obstacles = new List<Obstacle>(_count);
            var obstacleGroups = levelData.ObstacleGroups.Where((group) => group.MaxCount > 0).
                Select((group) => new ObjectCountInGroup(group, 1)).ToList();

            for (int i = 0; i < _count; ++i)
            {
                int temp = 30;
                int indexGroup;
                do
                {
                    indexGroup = Random.Range(0, obstacleGroups.Count);
                    --temp;
                }
                while (temp > 0 && obstacleGroups[indexGroup].Count >= levelData.ObstacleGroups[indexGroup].MaxCount);

                if (temp == 0)
                    break;

                ++obstacleGroups[indexGroup].Count;
            }

            for (int i = 0; i < _count; ++i)
            {
                if (obstacleGroups.Count == 0)
                    break;
                
                var indexObstacleGroup = Random.Range(0, obstacleGroups.Count);
                var obstacleGroup = obstacleGroups[indexObstacleGroup];

                var obstacleInfo = obstacleGroup.Info.Obstacles[Random.Range(0, obstacleGroup.Info.Obstacles.Length)];

                var obstacle = SpawnObstacle(obstacleGroup.Count, obstacleGroup.Info, obstacleInfo);
                obstacles.Add(obstacle);
                
                if (--obstacleGroup.Count == 0)
                    obstacleGroups.RemoveAt(indexObstacleGroup);
            }

            return obstacles;
        }

        public void PrepareForNextGeneration() { }

        private Obstacle SpawnObstacle(int i, ObstacleGroupInfo obstacleGroupInfo, ObstacleInfo obstacleInfo)
        {
            var stepOffset = obstacleGroupInfo.RectSpawn.width / obstacleGroupInfo.MaxCount;
            var offsetX = stepOffset * i;
            var obstaclePosition = new Vector2(offsetX + Random.Range(-stepOffset, stepOffset) * _offsetCoefficientFromXBetweenObjects,
                Random.Range(0, obstacleGroupInfo.RectSpawn.height)) + obstacleGroupInfo.RectSpawn.position;

            var obstacle = Object.Instantiate(obstacleInfo.Obstacle, obstaclePosition, Quaternion.identity, _levelGenerator.transform);

            float coefficientScale =
                (obstaclePosition.y - obstacleGroupInfo.RectSpawn.yMin) / obstacleGroupInfo.RectSpawn.height;
            obstacle.transform.localScale *= Mathf.Max(1f - coefficientScale, obstacleGroupInfo.MinScale);

            var interactiveObjectPrefab =
                obstacleInfo.PossibleObjectPrefabs[Random.Range(0, obstacleInfo.PossibleObjectPrefabs.Length)];
            
            obstacle.Init(Object.Instantiate(interactiveObjectPrefab));
            obstacle.RandomFlipSprite();

            return obstacle;
        }
    }
}
