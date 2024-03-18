using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChainEnable : MonoBehaviour
{
    public GameObject[] objectsToEnable;
    
    private GameObject lastObject;
    public int index = 0;

    void Start()
    {
        GameCounter.Instance.maxSteps = objectsToEnable.Length;

        lastObject = objectsToEnable[0];
        Next();
    }
    void CreateHandler()
    {
        if (lastObject.GetComponent<GazeAware>() != null)
        {
            lastObject.GetComponent<GazeAware>().OnClick.AddListener(Next);
        }
        if (lastObject.GetComponent<OnPlayEvent>() != null)
        {
            lastObject.GetComponent<OnPlayEvent>().OnPlay.AddListener(Next);
        }
    }
    public void Next()
    {
        Thread.Sleep(500);
        GameCounter.Instance.Answer(true);
        lastObject.SetActive(false);
        if (index < objectsToEnable.Length)
        {

            lastObject = objectsToEnable[index];
            lastObject.SetActive(true);
            CreateHandler();
            index++;
            
        }
    }
}
