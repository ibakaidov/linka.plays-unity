using System.Collections.Generic;
using Game9.Gameplay;
using UnityEngine;

namespace Game9.Levels
{
    public class LevelPlotContainer : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> _obstacles;

        public List<Obstacle> Obstacles => _obstacles;
    }
}
