using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FillTarget : FillGrid
{
    public Color DoneColor = Color.green;
    public Color CurrentColor = Color.yellow;
    public Color NextColor = Color.red;

    private int current = 0;
    
    public int Current
    {
        get { return current; }
        set
        {
            if (current != value)
            {
                current = value;
                Colorize();
            }
        }
    }

    void    Colorize() {
        for (int i = 0; i < Prefabs.Length; i++)
        {
            Prefabs[i]
                .GetComponent<TextMeshProUGUI>().color = i < current ? DoneColor : i == current ? CurrentColor:NextColor;
        }
    
    }
    public override void Fill(string[] strings) {
        current = 0;
        base.Fill(strings);
        Colorize();
    
    }
}
