using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
using UnityEngine.Events;

public class Cursor : MonoBehaviour
{
    public static Cursor Instance
    {
        get
        {
            return FindObjectOfType<Cursor>();
        }
    }
    public float Timeout = 1000f;

    private int progress = 0;

    Image image;
    GazeAware LastInside;
    private RectTransform rectTransform;
    private RectTransform TimerCircle;
    private float enterTime;


    private Color initColor;


    public UnityEvent OnObjectEnter;
    public UnityEvent OnObjectExit;
    private GazeAware nowInside;

    public GazeAware NowInside
    {
        get
        {
            return nowInside;
        }
    }

    private Vector2 lastMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        TimerCircle = transform.Find("Timer").GetComponent<RectTransform>();
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        initColor = image.color;
        lastMousePosition = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        TimerCircle.localScale = Vector3.one * Mathf.Min(1f, progress / Timeout);
        image.color = initColor;
        var screen = Input.mousePosition;

        // float mouseDist = Vector2.Distance(lastMousePosition, screen);
        // lastMousePosition = screen;
#if !UNITY_EDITOR
        if (TobiiAPI.IsConnected )
        {
            var point = TobiiAPI.GetGazePoint();
            if (point.IsValid)
            {
                screen = point.Screen;
            }
        }
#endif
        rectTransform
            .position = screen;

        var objs = FindObjectsOfType<GazeAware>();
        nowInside = null;

        float minDist = float.MaxValue;
        foreach (var obj in objs)
        {
            var dist = Vector2.Distance(obj.GetSceenSpace().center, screen);

            if (dist < minDist)
            {
                minDist = dist;
                nowInside = obj;
            }
        }
        if (nowInside)
        {
            if (!nowInside.GetSceenSpace().Contains(screen))
            {
                nowInside = null;
            }
        }

        image.color = nowInside == null ? initColor : Color.red;
        if (LastInside == null)
        {
            if (nowInside != null)
            {
                OnEnter(nowInside);
            }
        }
        else
        {
            if (nowInside != null)
            {
                if (nowInside.UUID == LastInside.UUID)
                {
                    OnStay(nowInside);

                }
                else
                {
                    OnExit();
                }
            }
            else { OnExit(); }
        }

        LastInside = nowInside;
        if (nowInside != null)
        {
            rectTransform.position = nowInside.GetSceenSpace().center;
        }

    }

    private void OnExit()
    {
        if (progress == 0)
        {
            return;
        }
        OnObjectExit.Invoke();
        if (LastInside.enabled && LastInside.gameObject.activeInHierarchy)
        {
            LastInside.OnGazeExit.Invoke();
        }
        progress = 0;
    }

    private void OnStay(GazeAware nowInside)
    {
        var now = Time.fixedTime;
        progress = (int)((now - enterTime) * 1000f);
        if (nowInside.enabled && nowInside.gameObject.activeInHierarchy)
        {
            nowInside.OnGazeStay.Invoke(progress / Timeout);
        }
        if (progress > Timeout)
        {
            try
            {

                if (nowInside.OnClick != null)
                    nowInside.OnClick.Invoke();
                OnObjectExit.Invoke();

            }
            catch (System.Exception)
            {

                throw;
            }
            enterTime = now;
            progress = 0;
            LastInside = null;

        }
    }

    private void OnEnter(GazeAware nowInside)
    {
        enterTime = Time.fixedTime;
        if (nowInside.enabled && nowInside.gameObject.activeInHierarchy)
        {
            nowInside.OnGazeEnter.Invoke();
            OnObjectEnter.Invoke();
        }
    }
}
