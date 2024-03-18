using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyFather : MonoBehaviour
{
    public GameObject ButterflyPrefab;
    public RectTransform CursorPosition;
    public float changeDistance = 0.1f;

    private Vector3 prev = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var now = CursorPosition.position;

        if (Vector3.Distance(prev, now) > changeDistance)
        {
            var butterfly = Instantiate(ButterflyPrefab, transform);
            butterfly.transform.position = now;
            var r = Quaternion.FromToRotation(Vector3.up, now - prev);
            butterfly.transform.rotation = r;
            prev = now;

        }
        
    }
}
