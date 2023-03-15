using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game6.UI
{
    public class LineConfigurationView : MonoBehaviour
    {
        public event UnityAction<bool> Enabled
        {
            add => _enabled.onValueChanged.AddListener(value);
            remove => _enabled.onValueChanged.RemoveListener(value);
        }
        
        public event UnityAction<float> ChangedScale
        {
            add => _scale.onValueChanged.AddListener(value);
            remove => _scale.onValueChanged.RemoveListener(value);
        }

        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Toggle _enabled;
        [SerializeField] private Slider _scale;

        public void Init(string label, float defaultScale, Vector2 minMaxScale)
        {
            _label.text = label;
            _scale.value = defaultScale;
            _scale.minValue = minMaxScale.x;
            _scale.maxValue = minMaxScale.y;
        }
    }
}
