using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    namespace Game1
    {
        public class DeactivateEndAnimation : MonoBehaviour
        {
            [SerializeField] private GameObject[] gameObjects;

            private void Start() 
            {
                Debug.Log("Set press time: " + ForSetTime.gameOneOldTime);
                Settings.time = ForSetTime.gameOneOldTime;
            }

            public void deactivateEndAnimation()
            {
                //deactivate and animation and exit button
                Debug.Log("Deactivating end animation");
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    gameObjects[i].SetActive(false);
                }
            }
        }
    }
}
