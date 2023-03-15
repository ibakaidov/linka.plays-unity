using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game1
    {
        public class DeactivateAll : MonoBehaviour
        {
            [SerializeField] private GameObject b;
            [SerializeField] private GameObject f;
            [SerializeField] private GameObject buttonFly;
            [SerializeField] private GameObject buttonBee;

            public void Clk()
            {
                Debug.Log("Deactivate all Particle System and Audio");
                b.SetActive(false);
                f.SetActive(false);
                buttonBee.SetActive(true);
                buttonFly.SetActive(true);
            }
        }
    }
}
