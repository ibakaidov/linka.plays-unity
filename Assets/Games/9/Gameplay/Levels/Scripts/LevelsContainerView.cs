using System;
using System.Collections.Generic;
using Game9.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Game9.UI
{
    public class LevelsContainerView : MonoBehaviour
    {
        public event Action<int> Choiced;

        [SerializeField] private LevelView _levelButton;

        public void Init(IReadOnlyList<PlotLevelData> levels)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                var button = Instantiate(_levelButton, transform);
                button.Init(i, levels[i].Icon);
                button.Clicked += OnClicked;
            }
        }

        private void OnClicked(int index) => Choiced?.Invoke(index);
    }
}
