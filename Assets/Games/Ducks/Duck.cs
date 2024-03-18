using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        GetComponent<GazeAware>().OnClick.AddListener(() =>
        {
            GameCounter.Instance.Answer(true );
            Destroy(gameObject);
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
