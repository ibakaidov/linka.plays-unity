using System;
using Shell;
using UnityEngine;
using UnityEngine.UI;

public class HoldTimeChanger : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private float _lastTime;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(OnChangeValue);
    }

    private void OnEnable()
    {
        _lastTime = Settings.time;
        OnChangeValue(_slider.value);
    }

    private void OnDisable() => Settings.time = _lastTime;
    
    private static void OnChangeValue(float value) => Settings.time = value;
}
