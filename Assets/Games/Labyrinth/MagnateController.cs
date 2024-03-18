using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnateController : MonoBehaviour
{
    public float Speed = 1.5f;
    public Rigidbody BallRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<GazeAware>().OnGazeStay.AddListener((float a) =>
        {
            var diff = transform.position - BallRigidbody.transform.position;
            diff.y = 0;
            BallRigidbody.AddForce(diff.normalized * Speed);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
