using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class FillGrid : MonoBehaviour
{
    public GameObject Prefab;

    public GameObject[] Prefabs;

   public virtual void Fill(string value)
    {
        Fill(value.Select((x)=>new string(x, 1)).ToArray());
    }
    public virtual void Fill(string[] values)
    {
        Prefabs = new GameObject[values.Length];
        for (int i = 0; i < transform.childCount; i++)
        {
           Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = 0;i < values.Length; i++)
        {
            var g = GameObject.Instantiate(Prefab);
            g.GetComponentInChildren<TextMeshProUGUI>().text = values[i];
            g.transform.SetParent(transform);
            Prefabs[i] = g;
        }
    }

}
