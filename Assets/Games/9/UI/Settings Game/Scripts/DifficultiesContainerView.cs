using System;
using System.Collections.Generic;
using Game9.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Game9.UI
{
    public class DifficultiesContainerView : MonoBehaviour
    {
        public event Action<int> Choiced;
        public event Action<int> ChoicedLevel;

        [SerializeField] private DifficultyChoicerButtonView _difficultyChoicerButtonViewPrefab;
        [SerializeField] private LevelsContainerView _levelsContainerViewPrefab;
        [SerializeField] private GroupChoicer _groupChoicer;

        public void Init(IReadOnlyList<DifficultyData> difficultyDatas)
        {
            for (int i = 0; i < difficultyDatas.Count; i++)
            {
                var view = Instantiate(_difficultyChoicerButtonViewPrefab, _groupChoicer.transform);
                view.Init(difficultyDatas[i]);

                var levelsContainer = Instantiate(_levelsContainerViewPrefab, transform);
                _groupChoicer.AddGroup(view.GetComponent<Button>(), levelsContainer.gameObject);
                
                levelsContainer.Init(difficultyDatas[i].Datas);
                levelsContainer.Choiced += OnChoiced;
            }

            _groupChoicer.GroupChanged += (_, currentIndex) => Choiced?.Invoke(currentIndex);
        }
        
        private void OnChoiced(int index) => ChoicedLevel?.Invoke(index);
    }
}
