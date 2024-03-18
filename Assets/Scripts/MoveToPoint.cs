using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    [SerializeField]
    public RectTransform rectTransformTarget;
    [SerializeField]
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(target is null))
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(this);
            }
        }
        else if (!(rectTransformTarget is null))
        {
            transform.position = Vector3.Lerp(transform.position, rectTransformTarget.position, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, rectTransformTarget.position) < 0.1f)
            {
                Destroy(this);
            }
        }

    }
}
