using Game9.Levels;
using TMPro;
using UnityEngine;

namespace Game9.UI
{
    public class DifficultyChoicerButtonView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void Init(DifficultyData difficultyData)
        {
            _text.text = difficultyData.Name;
        }
    }
}
