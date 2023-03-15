using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game9
{
    public class GroupChoicer : MonoBehaviour
    {
        public event Action<int, int> GroupChanged;
        
        [SerializeField] private List<Button> _groups;
        [SerializeField] private List<GameObject> _containers;
        [SerializeField] private Color _disabledColor;
        [SerializeField] private Color _choicedColor;

        private int _currentIndex;

        public void AddGroup(Button group, GameObject container)
        {
            _groups.Add(group);
            _containers.Add(container);
            InitGroup(_groups.Count - 1);
        }

        private void InitGroup(int i)
        {
            _containers[i].gameObject.SetActive(_currentIndex == i);
            
            _groups[i].image.color = i == _currentIndex ? _choicedColor : _disabledColor;
            _groups[i].onClick.AddListener(() => OnClick(i));
        }
        
        private void Awake()
        {
            for (var i = 0; i < _groups.Count; i++)
                InitGroup(i);
        }

        private void OnClick(int index)
        {
            if (index == _currentIndex)
                return;
            
            int lastIndex = _currentIndex;
            _currentIndex = index;
            
            _groups[lastIndex].image.color = _disabledColor;
            _groups[_currentIndex].image.color = _choicedColor;

            _containers[lastIndex].SetActive(false);
            _containers[_currentIndex].SetActive(true);
            
            GroupChanged?.Invoke(lastIndex, _currentIndex);
        }
    }
}
