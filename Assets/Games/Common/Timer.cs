using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    public class Timer : MonoBehaviour
    {


        TimeSpan time;
        Text textComponent;
        // Start is called before the first frame update
        void Start()
        {
            gameObject.TryGetComponent<Text>(out textComponent);
        }

        private void OnEnable()
        {
            time = TimeSpan.Zero;
        }

        public void Stop()
        {
            time = TimeSpan.Zero;
        }

        // Update is called once per frame
        void Update()
        {
            time += TimeSpan.FromSeconds(Time.deltaTime);
            textComponent.text = time.ToString("mm\\:ss");
        }
    }
}


