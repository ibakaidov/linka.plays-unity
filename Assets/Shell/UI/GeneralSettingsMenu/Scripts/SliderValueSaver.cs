using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    public class SliderValueSaver : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private string _key;
        [SerializeField] private float _defaultValue = 0.5f;
        
        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnChangeValue);
            _slider.value = PlayerPrefs.GetFloat(_key, _defaultValue);
        }

        private void OnChangeValue(float value)
        {
            PlayerPrefs.SetFloat(_key, value);
            PlayerPrefs.Save();
        }
    }
}
