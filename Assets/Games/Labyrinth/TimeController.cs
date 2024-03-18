using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public CollisionEvent finishLine;
    public CollisionEvent firstSectorLine;  

    public TextMeshProUGUI infoText;  
    private float bestTime ;
    private float lastLap;

private int lap = 0;
    private float startTime = 0;
    private float firstSectorTime = 1;
    // Start is called before the first frame update
    void Start()
    {
        finishLine.OnCollision.AddListener(() =>
        {
            if(startTime<firstSectorTime){
                if(lap != 0){
                    if(bestTime == 0 || (Time.time - startTime) < bestTime){
                        bestTime = Time.time - startTime;
                    }
                    lastLap = Time.time - startTime;
                }
                lap++;
                GameCounter.Instance.Answer(true);
                startTime = Time.time;
            }
        });

        firstSectorLine.OnCollision.AddListener(() =>
        {
            firstSectorTime = Time.time;
        });
    }

    // Update is called once per frame
    void Update()
    {
        infoText.text = "Круг: " + lap;
        if(startTime > 0){
            infoText.text += "\nТекущий: " + (Time.time - startTime).ToString("F2");
        }
        if(lap > 0){
            infoText.text += "\nПоследний: " + lastLap.ToString("F2");
        }
        if(bestTime > 0){
            infoText.text += "\nЛучший: " + bestTime.ToString("F2");
        }

    }
}
