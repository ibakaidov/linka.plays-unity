using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    Animator animator;
    public float radius = 2;
    public float velocity = 1;

    private Vector2 center;
    private Vector2 target;

    private AudioSource sound;

    // Start is called before the first frame update
    void Start() 
    {
        animator = GetComponent<Animator>();
        center = transform.position;
        target = center + Random.insideUnitCircle * radius;
        var t1 = transform.localScale;
        t1.x = ((target - center).x < 0 ? 1 : -1) * Mathf.Abs(t1.x);
        transform.localScale = t1;

        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SightOver(float t)
    {
        Swim();
    }

    public void SightEnter()
    {
        sound.Play();
        animator.SetBool("MouseOver",true);
    }

    public void SightExit()
    {
        sound.Stop();
        animator.SetBool("MouseOver", false);
    }


    private void Swim()
    {
        
        var t = (Vector2)transform.position;
        if (Vector2.Distance(t, target) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(t, target, velocity * Time.deltaTime);
        }
        else
        {
            target = center + Random.insideUnitCircle * radius;
            var t1 = transform.localScale;
            t1.x = ((target - t).x < 0 ? 1 : -1) * Mathf.Abs(t1.x);
            transform.localScale = t1;
        }
    }
}
