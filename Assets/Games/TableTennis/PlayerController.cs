using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public bool AI;
    public GameObject ball;
    public float speed = 10;

    public float topBound = 4F;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AI)
        {
            AIControl();
        }
        else
        {
            PlayerControl();
        }

    }
    void AIControl()
    {
        if (ball.transform.position.y > transform.position.y)
        {
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
        if (ball.transform.position.y < transform.position.y)
        {
            transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        }
        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        }
        if (transform.position.y < -topBound)
        {
            transform.position = new Vector3(transform.position.x, -topBound, transform.position.z);
        }
    }
    void PlayerControl()
    {
        var point = Input.mousePosition;

#if !UNITY_EDITOR
        if (TobiiAPI.IsConnected)
        {
            point = TobiiAPI.GetGazePoint().Screen;
        }
#endif
        var worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(point.x, point.y, -Camera.main.transform.position.z));
        transform.position = new Vector3(transform.position.x, worldPoint.y, transform.position.z);

        if (transform.position.y > topBound)
        {
            transform.position = new Vector3(transform.position.x, topBound, transform.position.z);
        }
        if (transform.position.y < -topBound)
        {
            transform.position = new Vector3(transform.position.x, -topBound, transform.position.z);
        }
    }
}
