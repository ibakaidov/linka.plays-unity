using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public Animator animator;
    public static AudioSource soundOpen;
    public static AudioSource soundJiggle;
    // Start is called before the first frame update
    void Start()
    {
        var t = GetComponents<AudioSource>();
        soundOpen = t[0];
        soundJiggle = t[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SightEnter()
    {
        animator.SetBool("MouseOver", true);
    }

    public void SightExit()
    {
        animator.SetBool("MouseOver", false);
    }
}
