using System.Collections;
using UnityEngine;

namespace Game6
{
    [RequireComponent(typeof(Animation))]
    public class WaitFromStartAnimation : MonoBehaviour
    {
        [SerializeField] private float _waitTime;
        
        IEnumerator Start()
        {
            yield return new WaitForSeconds(_waitTime);
            GetComponent<Animation>().Play();
        }
    }
}
