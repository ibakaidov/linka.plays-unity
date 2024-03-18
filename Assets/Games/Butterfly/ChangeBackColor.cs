using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackColor : MonoBehaviour
{
    public Color color;
    private Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        camera.backgroundColor = Color.black;
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.backgroundColor = Color.Lerp(camera.backgroundColor, color, Time.deltaTime*5f);
        
    }

    public void ChangeColor(string color)
    {
        
        //hex to color
        ColorUtility.TryParseHtmlString(color, out var c);
        
        this.color = c;
    }   
}
