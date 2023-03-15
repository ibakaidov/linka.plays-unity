using Game9.Gameplay;
using UnityEngine;

namespace Game9.Levels
{
    [System.Serializable]
    public class ObstacleGroupInfo
    {
        [SerializeField] private Rect _rectSpawn;
        [SerializeField] private int _maxCount = 2;
        [SerializeField] private float _minScale = 0.5f;
        [SerializeField] private ObstacleInfo[] _obstacles;
        
        public Rect RectSpawn => _rectSpawn;
        public int MaxCount => _maxCount;
        public float MinScale => _minScale;
        public ObstacleInfo[] Obstacles => _obstacles;
    }
    
    [System.Serializable]
    public class ObstacleInfo
    {
        [SerializeField] private Obstacle _obstacle;
        [SerializeField] private BaseInteractiveObject[] _possibleObjectPrefabs;
        
        public Obstacle Obstacle => _obstacle;
        public BaseInteractiveObject[] PossibleObjectPrefabs => _possibleObjectPrefabs;
    }
    
    [CreateAssetMenu(fileName = "RandomLevelData", menuName = "Game 9/LevelData")]
    public class RandomLevelData : ScriptableObject
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private ObstacleGroupInfo[] _obstacleGroups;

        public GameObject Background => _background;
        public ObstacleGroupInfo[] ObstacleGroups => _obstacleGroups;

        #if UNITY_EDITOR
        private void OnEnable() => UnityEditor.SceneView.duringSceneGui += OnUpdate;
        private void OnDisable() => UnityEditor.SceneView.duringSceneGui -= OnUpdate;

        private void OnUpdate(UnityEditor.SceneView _)
        {
            if (UnityEditor.Selection.activeObject != this)
                return;
            
            for (int i = 0; i < _obstacleGroups.Length; ++i)
            {
                var obstacleRect = _obstacleGroups[i].RectSpawn;

                var textStyle = new GUIStyle(GUI.skin.label);
                textStyle.fontSize = 22;

                UnityEditor.Handles.Label(obstacleRect.min + Vector2.up * 0.5f, i.ToString(), textStyle);
                UnityEditor.Handles.DrawWireCube(obstacleRect.center, obstacleRect.size);
            }
        }
        #endif
    }
}
