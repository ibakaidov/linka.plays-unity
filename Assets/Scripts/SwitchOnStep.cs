using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchOnStep : MonoBehaviour
{
    public int step;
    public UnityEvent onEqual;
    // Start is called before the first frame update
    void Start()
    {
        GameCounter
        .Instance
        .OnStep.AddListener(OnStep);
        
    }
    void OnStep(int step){
        if(step == this.step){
            
            onEqual.Invoke();   
        }
    }
}
