using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusController : MonoBehaviour
{
    Animator animator;
    GazeAware gazeAware;
    OnAnimationEnd onAnimationEnd;

    Vector3 TargetPosition = Vector3.zero; 
    static int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        animator = GetComponent<Animator>();
        gazeAware = GetComponent<GazeAware>();
        onAnimationEnd = GetComponent<OnAnimationEnd>();

        gazeAware.OnClick.AddListener(OnClick);
        onAnimationEnd.onEnd.AddListener(OnAnimationEnd);
    }
    void OnClick()
    {
        GameCounter.Instance.Answer(count.ToString()==gameObject.name);
        count++;
        animator.enabled = true;
    }
    void OnAnimationEnd()
    {
        animator.enabled = false;
        TargetPosition = new Vector3(0, (0.5f + (count*0.275f)), 0);
        Destroy(onAnimationEnd);
        Destroy(gazeAware);
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetPosition != Vector3.zero)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * 5);
        }
        
    }
}
