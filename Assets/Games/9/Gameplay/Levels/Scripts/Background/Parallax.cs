using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Parallax
    {
        private const float _sizeChunk = 17.5f;
        private const float _halfSizeChunk = _sizeChunk / 2f;

        [System.Serializable]
        private class Background
        {
            [SerializeField] private Transform _transform;
            [SerializeField, Range(0f, 2f)] private float _coefficientChange;
            [SerializeField] private List<Transform> _chunks;

            public Transform Transform => _transform;
            public float CoefficientChange => _coefficientChange;
            public List<Transform> Chunks => _chunks;
        }

        [SerializeField] private List<Background> _backgrounds;

        public void Update(float deltaPosition)
        {
            for (int i = 0; i < _backgrounds.Count; i++)
            {
                var background = _backgrounds[i];
                
                for (int j = 0; j < background.Chunks.Count; j++)
                {
                    background.Chunks[j].position += Vector3.right * (deltaPosition * background.CoefficientChange);
                }

                var firstChunk = background.Chunks[0];
                if (firstChunk.position.x >= -5f)
                {
                    var lastChunk = background.Chunks[background.Chunks.Count - 1];
                    background.Chunks.RemoveAt(background.Chunks.Count - 1);
                    background.Chunks.Insert(0, lastChunk);

                    Vector3 position = firstChunk.position;
                    position.x -= _sizeChunk;
                    lastChunk.position = position;
                }
            }
        }
    }
}
