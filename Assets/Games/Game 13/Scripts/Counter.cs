using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{

    public int MaxCount = 5;
    public int count = 0;
    [SerializeField] public UnityEvent EndCount;

    public void Increment()
    {
        count++;
        if(count >= MaxCount)
        {
            Debug.Log("End");

            EndCount.Invoke();
            //Destroy(this);
        }
    }
}
