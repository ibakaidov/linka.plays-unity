using System;
using System.Collections;
using UnityEngine;

namespace Game6
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] private float _time;
        
        IEnumerator Start()
        {
            yield return new WaitForSeconds(_time);
            Destroy(gameObject);
        }

        private void OnDisable() => Destroy(gameObject);
    }
}
