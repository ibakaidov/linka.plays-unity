using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrechToController : MonoBehaviour
{

    public GameObject StartNode;
    public GameObject EndNode;

    bool Scaled = false;
    void FixedUpdate()
    {
        bool v = Cursor.Instance.NowInside != null;
        GetComponent<SpriteRenderer>().enabled = v;
        if (v)
        {
            EndNode.transform.position = Cursor.Instance.NowInside.transform.position;
        } else {
            
        }
        if (StartNode.transform.hasChanged || EndNode.transform.hasChanged)
        {
            Scale();
        }
    }
    void Scale()
    {
        Vector3 centerPos = (StartNode.transform.position + EndNode.transform.position) / 2f;
        transform.position = centerPos;
        Vector3 direction = EndNode.transform.position - StartNode.transform.position;
        direction = Vector3.Normalize(direction);
        transform.right = direction;
        Vector3 scale = new Vector3(1, 1, 1);
        scale.x = Vector3.Distance(StartNode.transform.position, EndNode.transform.position);
        transform.localScale = scale;
    }
}
