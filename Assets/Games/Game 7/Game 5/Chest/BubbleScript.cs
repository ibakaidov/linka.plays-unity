using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    public float speed;
    public Vector2 target;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(target, transform.position) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            target = new Vector2(target.x + Random.value - 0.5f, target.y + Random.value);
        }
    }
}
