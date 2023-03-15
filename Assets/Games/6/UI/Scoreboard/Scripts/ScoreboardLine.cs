using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game6.UI
{
    public class ScoreboardLine : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void Init(LineUIData lineData)
        {
            _icon.sprite = lineData.Icon;
            ClearScore();
        }

        private int _score;

        public void IncrementScore() => _scoreText.text = (++_score).ToString();
        
        public void ClearScore()
        {
            _score = 0;
            _scoreText.text = "0";
        }
    }
}
