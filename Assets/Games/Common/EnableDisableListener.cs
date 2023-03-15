using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableListener : MonoBehaviour
{
    public Action Enable = delegate { };

    public Action Disable = delegate { };

    private void OnEnable()
    {
        Enable();
    }
    private void OnDisable()
    {
        Disable();
    }
}
