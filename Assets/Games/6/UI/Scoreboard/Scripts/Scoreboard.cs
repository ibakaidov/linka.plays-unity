using System.Collections.Generic;
using UnityEngine;

namespace Game6.UI
{
    public class Scoreboard : MonoBehaviour
    {
        [SerializeField] private ScoreboardLine _prefab;
        [SerializeField] private RectTransform _container;

        private readonly Dictionary<LineInfo, ScoreboardLine> _lines = new();

        public void Add(LineInfo lineInfo)
        {
            if (_lines.ContainsKey(lineInfo))
                Remove(lineInfo);
            
            var line = Instantiate(_prefab, _container);
            line.Init(lineInfo.UIData);
            _lines.Add(lineInfo, line);
        }

        public void Remove(LineInfo lineInfo)
        {
            var line = _lines[lineInfo];
            Destroy(line.gameObject);
            _lines.Remove(lineInfo);
        }

        public void IncrementScore(LineInfo lineInfo) => _lines[lineInfo].IncrementScore();
    }
}
