using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropController : MonoBehaviour
{
    private Image image;
    
   
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void Amount(float d) {
        image.color = Color.Lerp(Color.white, Color.cyan, d*0.5F);
    }
}
