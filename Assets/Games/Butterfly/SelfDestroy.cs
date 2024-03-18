using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float Timeout = 1000f;
    
    private float birthTime;

    void Start()
    {
        birthTime = Time.time;
    }

    void Update()
    {
        if (Time.time - birthTime > Timeout/1000f)
        {
            Destroy(gameObject);
        }
    }

}
