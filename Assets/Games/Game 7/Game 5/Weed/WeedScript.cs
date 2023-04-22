using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedScript : MonoBehaviour
{
    Animator animator;
    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SightEnter()
    {
        animator.SetBool("MouseOver", true);
        sound.Play();
    }

    public void SightExit()
    {
        animator.SetBool("MouseOver", false);
        sound.Stop();
    }
}
