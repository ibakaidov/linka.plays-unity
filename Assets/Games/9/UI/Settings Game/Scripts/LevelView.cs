using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game9
{
    [RequireComponent(typeof(Button))]
    public class LevelView : MonoBehaviour
    {
        public event Action<int> Clicked;
        
        private Button _button;
        private int _levelIndex;

        public void Init(int levelIndex, Sprite sprite)
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClicked);
            
            _button.image.sprite = sprite;
            _levelIndex = levelIndex;
        }
        
        private void OnClicked() => Clicked?.Invoke(_levelIndex);
    }
}
