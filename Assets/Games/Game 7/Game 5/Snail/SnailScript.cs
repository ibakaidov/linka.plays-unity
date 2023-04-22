using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnailScript : MonoBehaviour
{
    public float speed = 0.2f, animationSpeed = 1;
    Vector3 left, right;
    bool toRight = true;
    SpriteRenderer sprite;
    Animator animator;

    private void Start()
    {
        left = transform.position;
        right = transform.position;
        right.x = -right.x;
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        animator.speed = 0;
    }

    public void SightEnter()
    {
        animator.speed = animationSpeed;
    }

    public void SightExit()
    {
        animator.speed = 0;
    }

    public void SightOver()
    {
        if (toRight)
        {
            if (Vector2.Distance(transform.position, right) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, right, Time.deltaTime * speed);
            }
            else
            {
                toRight = false;
                sprite.flipX = true;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, left) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, left, Time.deltaTime * speed);
            }
            else
            {
                toRight = true;
                sprite.flipX = false;
            }
        }
    }
}
