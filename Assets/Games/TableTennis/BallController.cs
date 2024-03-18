using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.right * 250);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
         // Get position of collision
        Vector3 hitPoint = other.contacts[0].point;
        // Get position of paddle
        Vector3 paddlePos = other.gameObject.transform.position;
        // Calculate the direction of the ball
        float y = hitPoint.y - paddlePos.y;


        if (other.gameObject.name == "AI")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(1, y, 0) * 250);
        }
        if (other.gameObject.name == "Player")
        {
            rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(new Vector3(-1, y, 0) * 250);
        }
     
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "RScore" || other.gameObject.name == "LScore")
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(0, 0, 0);
            rb.AddForce(Vector3.right * 250);
        }

    }
}
