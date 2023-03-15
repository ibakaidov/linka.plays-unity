using System;
using TMPro;
using UnityEngine;

namespace Game9.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private bool _isUpdate;
        private float _startTime;
        private float _saveTimeFromDisableObject;

        public void Continue() => _isUpdate = true;
        public void Stop() => _isUpdate = false;
        public void ResetTime() => _startTime = Time.time;

        private void Update()
        {
            if (_isUpdate)
                _text.text = (Time.time - _startTime).ToString("f1");
            else
                _startTime += Time.deltaTime;
        }

        private void OnDisable() => _saveTimeFromDisableObject = Time.time;
        
        private void OnEnable() => _startTime += Time.time - _saveTimeFromDisableObject;
    }
}
