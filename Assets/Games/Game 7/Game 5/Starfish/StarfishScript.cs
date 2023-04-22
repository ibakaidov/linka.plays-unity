using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishScript : MonoBehaviour
{
    public Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SightEnter()
    {
        sound.Play();
        animator.SetTrigger("OnMouseOver");
    }
}
