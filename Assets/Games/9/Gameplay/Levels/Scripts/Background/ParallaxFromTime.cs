using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ParallaxFromTime : MonoBehaviour
    {
        [SerializeField] private Parallax _parallax;
        [SerializeField] private float _speed;

        private Vector2 _position;

        private void Update()
        {
            _parallax.Update(_speed * Time.deltaTime);
        }
    }
}