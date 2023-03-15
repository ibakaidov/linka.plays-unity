using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game6
{
    [System.Serializable]
    public class LineData
    {
        [SerializeField] private float _defaultScale = 1f;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private InteractiveObject _interactiveObject;
        [SerializeField] private Sprite[] _sprites;
        
        public float DefaultScale => _defaultScale;
        public float Speed => _speed;
        public InteractiveObject InteractiveObject => _interactiveObject;
        public IReadOnlyList<Sprite> Sprites => _sprites;
    }
}
