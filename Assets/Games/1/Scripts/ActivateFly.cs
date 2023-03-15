using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game1
    {
        public class ActivateFly : MonoBehaviour
        {
            [SerializeField] private GameObject b;
            [SerializeField] private GameObject f;
            [SerializeField] private GameObject buttonFly;
            [SerializeField] private GameObject buttonBee;
            
            public void Clk()
            {
                Debug.Log("Activate fly animation");
                f.SetActive(true);
                b.SetActive(false);
                buttonFly.SetActive(false);
                buttonBee.SetActive(true);
            }

        }
    }
}