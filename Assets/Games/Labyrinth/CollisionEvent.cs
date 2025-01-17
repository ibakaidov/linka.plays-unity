using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent OnCollision;
    
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision");
        OnCollision.Invoke();
    }
}
