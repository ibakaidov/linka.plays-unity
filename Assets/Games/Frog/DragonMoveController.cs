using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonMoveController : MonoBehaviour
{
    public float speed = 0.5f;
    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * -Time.deltaTime * speed;
        if (Mathf.Abs(transform.position.x) > 10)
        {
            GameCounter.Instance.Answer(false);
            OnDestroy.Invoke();
            Destroy(gameObject);
        }
    }

    public void DestroySelf()
    {
        OnDestroy.Invoke();
        GameCounter.Instance.Answer(true);
        Destroy(gameObject);
    }
}
