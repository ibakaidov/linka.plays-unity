using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using Shell;

public class InsanceOnGaze : MonoBehaviour
{
    public GameObject prefab;
    public AudioSource source;
    private Vector2 lastPos;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        var pos = SightMaster.Point;


        //var pos = S point.Screen;
        var buttefly = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        buttefly.transform.SetParent(transform);



        if (lastPos != null)
        {
            buttefly.transform.LookAt(lastPos);
        }
        lastPos = pos;

    }
}