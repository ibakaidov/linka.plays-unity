using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Shell
{
    public class MixerValueChanger : MonoBehaviour
    {
        private const float _maxVolume = 0f;
        private const float _zeroVolume = -80f;

        [SerializeField] private AudioMixerGroup _mixerGroup;
        [SerializeField] private Slider _slider;

        private string NameParameterInMixer => $"{_mixerGroup.name}Volume";

        private float _lastValue;

        private void Awake()
        {
            _slider.onValueChanged.AddListener(OnChangeVolume);
        }

        private void OnEnable()
        {
            _mixerGroup.audioMixer.GetFloat(NameParameterInMixer, out _lastValue);
            OnChangeVolume(_slider.value);
        }

        private void OnDisable()
        {
            _mixerGroup.audioMixer.SetFloat(NameParameterInMixer, _lastValue);
        }

        private void OnChangeVolume(float value) =>
            _mixerGroup.audioMixer.SetFloat(NameParameterInMixer, ValueToVolume(value));

        private static float ValueToVolume(float value) =>
            Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * (_maxVolume - _zeroVolume) / 4f + _maxVolume;
    }
}
