using UnityEngine;

namespace Game9.UI
{
    public class InfoWindow : MonoBehaviour
    {
        [SerializeField] private Timer _timer;
        [SerializeField] private CounterObjects _counterObjects;
        
        public Timer Timer => _timer;
        public CounterObjects CounterObjects => _counterObjects;
    }
}
