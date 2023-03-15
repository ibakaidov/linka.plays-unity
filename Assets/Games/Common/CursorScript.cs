using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 npos = Camera.main.ScreenToWorldPoint(Shell.SightMaster.Point);
        npos.z = 1;
        transform.position = npos;
    }
}
