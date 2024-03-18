using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class GazeAware : MonoBehaviour
{
    [SerializeField]
    public int RECT_SIZE = 100;
    private string uuid;

    [SerializeField]
    public UnityEvent OnClick;
    [SerializeField]
    public UnityEvent OnGazeEnter;
    [SerializeField]
    public UnityEvent OnGazeExit;
    [SerializeField]
    public UnityEvent<float> OnGazeStay;

    [SerializeField]
    public bool Click = true;
    [SerializeField]
    public bool Select = false;
    [SerializeField]
    public bool Once = false;

    private void Start()
    {
        uuid = Guid.NewGuid().ToString();
        var self = gameObject;
        OnClick.AddListener(() =>
        {
            Button b = GetComponent<Button>();
            if (b != null && Click)
            {
                if (!Select) {
                    b.
                    onClick
                    .Invoke();
                } else {
                    if(EventSystem.current.currentSelectedGameObject == self)
                    {
                        b.onClick.Invoke();
                    }
                    else {
                        EventSystem.current.SetSelectedGameObject(self);
                    }
                }
            }
            if (Once)
            {
                enabled = false;
            }
        });
    }

    public string UUID
    {
        get
        {
            return uuid;
        }
    }

    public Rect GetSceenSpace()
    {
        // get dpi
        float dpi = Screen.dpi;
        var rect_size = RECT_SIZE;

        if(!enabled||!gameObject.activeInHierarchy)
        {
            return new Rect();
        }
        if(Camera.main == null)
        {
            Debug.LogError("No main camera found");
            return new Rect();
        }
        var point = Camera.main.WorldToScreenPoint(transform.position);
        if (GetComponent<RectTransform>() != null)
        {
            var r = RectTransformToScreenSpace(GetComponent<RectTransform>());
            point = r.center;
            if (r.width > rect_size)
            {
                return r;
            }
        }
        if (GetComponent<Collider>() != null)
        {
            var r = GetComponent<Collider>().bounds;
            point = Camera.main.WorldToScreenPoint(r.center);
            var lefttop = Camera.main.WorldToScreenPoint(r.min);
            var rightbottom = Camera.main.WorldToScreenPoint(r.max);
            var width = rightbottom.x - lefttop.x;
            var height = rightbottom.y - lefttop.y;
            return new Rect(lefttop.x, lefttop.y, width, height);
        }

        var half = rect_size / 2;
        var rect = new Rect(point.x - half, point.y - half, rect_size, rect_size);
        return rect;
    }
    public static Rect RectTransformToScreenSpace(RectTransform transform)
    {
        Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
        return new Rect((Vector2)transform.position - (size * 0.5f), size);
    }
}
