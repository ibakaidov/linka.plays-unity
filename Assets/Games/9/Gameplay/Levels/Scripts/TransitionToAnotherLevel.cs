using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class TransitionToAnotherLevel : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] _objectPrefabs;
    [SerializeField] private uint _countObjects = 100;

    private ObjectPool<Rigidbody2D> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Rigidbody2D>(() => Instantiate(_objectPrefabs[Random.Range(0, _objectPrefabs.Length)], transform),
            actionOnGet: (rigidbody) => rigidbody.gameObject.SetActive(true),
            actionOnRelease: (rigidbody) => rigidbody.gameObject.SetActive(false),
            defaultCapacity: (int)_countObjects);
        Action();
    }

    private void Action()
    {
        for (uint i = 0; i < _countObjects; ++i)
            _pool.Get().position = new Vector2(Random.Range(-10f, 10f), Random.Range(-5f, 5f));
    }
}
