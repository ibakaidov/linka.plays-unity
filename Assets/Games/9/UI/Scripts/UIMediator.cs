using System;
using System.Collections.Generic;
using Game9.Levels;
using Game9.UI;
using Shell;
using UnityEngine;

namespace Game9
{
    public class UIMediator : MonoBehaviour
    {
        public static event Action<int> ChoicedLevel
        {
            add => _instance._settingsForGame.DifficultiesContainerView.ChoicedLevel += value;
            remove => _instance._settingsForGame.DifficultiesContainerView.ChoicedLevel -= value;
        }
        
        public static event Action<int> ChoicedDifficulty
        {
            add => _instance._settingsForGame.DifficultiesContainerView.Choiced += value;
            remove => _instance._settingsForGame.DifficultiesContainerView.Choiced -= value;
        }
        
        public static event Action<int, int> ChoicedLevelGeneratorType
        {
            add => _instance._settingsForGame.ChoicerLevelGeneratorType.GroupChanged += value;
            remove => _instance._settingsForGame.ChoicerLevelGeneratorType.GroupChanged -= value;
        } 
        
        private static UIMediator _instance;
        
        [SerializeField] private EyeTrackerHandler _eyeHandler;
        [SerializeField] private InfoWindow _infoWindow;
        [SerializeField] private SettingsForGame _settingsForGame;

        private void Awake() => _instance = this;
        private void OnDestroy() => _instance = null;

        public static void RegisterEyeButton(IEyeUpdater currentEye, Vector2 position)
        {
            _instance._eyeHandler.transform.position = Camera.main.WorldToScreenPoint(position);
            _instance._eyeHandler.AddEyeTracker(currentEye);
        }
        
        public static void UnregisterEyeButton(IEyeUpdater currentEye)
        {
            _instance._eyeHandler.RemoveEyeTracker(currentEye);
        }

        public static void ContinueTimer() => _instance._infoWindow.Timer.Continue();
        public static void StopTimer() => _instance._infoWindow.Timer.Stop();
        public static void ResetTimer() => _instance._infoWindow.Timer.ResetTime();

        public static void InitCounterObjects(int maxValue) => _instance._infoWindow.CounterObjects.Init(maxValue);
        public static void DecrementCounterObjects() => _instance._infoWindow.CounterObjects.Decrement();

        public static void InitLevelsContainer(IReadOnlyList<DifficultyData> levels) =>
            _instance._settingsForGame.DifficultiesContainerView.Init(levels);
    }
}
