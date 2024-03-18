using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[DisallowMultipleComponent]
public class ButtonToggle : MonoBehaviour
{

    public bool isOn = false;

    Color DefaultColor;

    public Color OnColor = Color.green;

    public UnityEvent<bool> OnToggle;
    public UnityEvent OnToggleOn;
    public UnityEvent OnToggleOff;

    // Start is called before the first frame update
    void Start()
    {
    
        DefaultColor = GetComponent<Image>().color;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            isOn = !isOn;
            OnToggle.Invoke(isOn);
            if (isOn)
            {
                OnToggleOn.Invoke();
            }
            else
            {
                OnToggleOff.Invoke();
            }

        });

    }

    // Update is called once per frame
    void Update()
    {

        if (isOn)
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, OnColor, Time.deltaTime * 5);
        }
        else
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, DefaultColor, Time.deltaTime * 5);
        }
    }
}
