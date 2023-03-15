using TMPro;
using UnityEngine;

namespace Game9.UI
{
    public class CounterObjects : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private int _maxCount;
        private int _value;

        public void Init(int maxCount)
        {
            _maxCount = maxCount;
            _value = _maxCount;
            UpdateValue();
        }

        public void Decrement()
        {
            --_value;
            UpdateValue();
        }

        private void UpdateValue() => _text.text = $"{_value}/{_maxCount}";
    }
}
