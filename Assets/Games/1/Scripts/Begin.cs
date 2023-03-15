using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    namespace Game1
    {

        public static class ForSetTime
        {
            public static float gameOneOldTime = Settings.time;
        }

        public class Begin : MonoBehaviour
        {
            [SerializeField] private GameObject beeButton;
            [SerializeField] private GameObject flyButton;
            [SerializeField] private GameObject beeExitButton;
            [SerializeField] private GameObject flyExitButton;
            [SerializeField] private AudioSource audioBee;
            [SerializeField] private AudioSource audioFly;
            [SerializeField] private GameObject psBee;
            [SerializeField] private GameObject psFly;
            [SerializeField] private GameObject endAnimation;

            public static float gameOneOldTime = Settings.time;

            public void deactivateAll()
            {
                Debug.Log("Deactivate all buttons");
                beeButton.SetActive(false);
                flyButton.SetActive(false);
                beeExitButton.SetActive(false);
                flyExitButton.SetActive(false);
                psBee.SetActive(false);
                psFly.SetActive(false);
                Debug.Log("Set press time: " + gameOneOldTime);
                Settings.time = gameOneOldTime;
                endAnimation.SetActive(false);
            }

            public void activateBeeAudio()
            {
                Debug.Log("Activate bee audio");
                audioBee.Play();
            }

            public void activateFlyAudio()
            {
                Debug.Log("Activate fly audio");
                audioFly.Play();
            }

            public void deactivateBeeAudio()
            {
                Debug.Log("Stop bee audio");
                audioBee.Stop();
            }

            public void deactivateFlyAudio()
            {
                Debug.Log("Stop fly audio");
                audioFly.Stop();
            }

            public void activateAllButtons()
            {
                Debug.Log("Activate all buttons");
                beeButton.SetActive(true);
                flyButton.SetActive(true);
                beeExitButton.SetActive(true);
                flyExitButton.SetActive(true);
                Debug.Log("Old press time: " + gameOneOldTime);
                Settings.time = 0.2f;
                Debug.Log("Set new press time: " + 0.2f);
            }
        }
    }
}
