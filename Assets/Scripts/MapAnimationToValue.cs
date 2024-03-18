using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAnimationToValue : MonoBehaviour
{
    public AnimationClip clip;

    public float min = 0;
    public float max = 1;


    float value = 0;

    void Update()
    {
        value = Mathf.Lerp(value, min, Time.deltaTime);
        clip.SampleAnimation(gameObject, value);
    }

    public void SetValue(float value)
    {
        this.value = value; 
        clip.SampleAnimation(gameObject, value);
    }
}
