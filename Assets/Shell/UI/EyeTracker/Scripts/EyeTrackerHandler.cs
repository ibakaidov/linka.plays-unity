using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    public class EyeTrackerHandler : MonoBehaviour
    {
        public static event Action ClickedEye;
        public static event Action<float> ChangeRatio;

        [SerializeField] private float _speedDecreaseRatio = 0.8f;
        private float _currentTime;
        
        private IEyeUpdater _lastEyeTracker;
        private bool _isSeen;

        private readonly List<IEyeUpdater> _eyeTrackers = new();

        private IEyeUpdater CurrentTracker => _eyeTrackers[0];

        public void AddEyeTracker(IEyeUpdater eyeTracker)
        {
            _eyeTrackers.Add(eyeTracker);
            if (_eyeTrackers.Count == 1)
                SetEyeTracker(eyeTracker);
        }

        public void RemoveEyeTracker(IEyeUpdater eyeTracker)
        {
            if (CurrentTracker == eyeTracker)
                RemoveFirst();
            else
                _eyeTrackers.Remove(eyeTracker);

            if (_eyeTrackers.Count != 0)
                SetEyeTracker(CurrentTracker);
            else
                _isSeen = false;
        }

        private void SetEyeTracker(IEyeUpdater eyeTracker)
        {
            _isSeen = true;

            if (eyeTracker != _lastEyeTracker)
                _currentTime = 0f;
        }

        private void RemoveFirst()
        {
            var eyeTracker = CurrentTracker;
            _eyeTrackers.RemoveAt(0);
            
            _lastEyeTracker = eyeTracker;
        }

        private void OnDestroy()
        {
            ClickedEye = null;
            ChangeRatio = null;
        }

        private void Update() => Control();

        private void Control() {
            while (_eyeTrackers.Count > 0 && CurrentTracker.IsValid == false)
                RemoveEyeTracker(CurrentTracker);

            if (_isSeen)
            {
                _currentTime += Time.deltaTime;
                if (_currentTime >= Settings.time)
                {
                    ClickedEye?.Invoke();
                    CurrentTracker.ClickFromEye();
                    _currentTime = 0;
                }
            }
            else
            {
                _currentTime = Mathf.Max(_currentTime - _speedDecreaseRatio * Time.deltaTime, 0f);
            }

            float ratio = _currentTime / Settings.time;
            if (_eyeTrackers.Count != 0)
                CurrentTracker.UpdateEyeRation(ratio);
            ChangeRatio?.Invoke(ratio);
        }
    }
}
