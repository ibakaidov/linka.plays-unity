using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game1
    {
        public class ActivateBee : MonoBehaviour
        {
            [SerializeField] private GameObject b;
            [SerializeField] private GameObject f;
            [SerializeField] private GameObject buttonFly;
            [SerializeField] private GameObject buttonBee;
            
            public void Clk()
            {
                Debug.Log("Activate bee animation");
                f.SetActive(false);
                b.SetActive(true);
                buttonBee.SetActive(false);
                buttonFly.SetActive(true);
            }

        }
    }
}
