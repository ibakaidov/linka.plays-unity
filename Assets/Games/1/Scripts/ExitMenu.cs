using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    namespace Game1
    {
        public class ExitMenu : MonoBehaviour
        {
            [System.Serializable]
            public class Window 
            {
                [SerializeField] private GameObject go;
                [SerializeField] private GameObject ui;

                public GameObject GO {
                    get => go;
                }

                public GameObject UI {
                    get => ui;
                }

                public void SetActive(bool active) {
                    go.SetActive(active);
                    ui.SetActive(active);
                }
            }

            [SerializeField] private ParticleSystem activePS;
            [SerializeField] private GameObject[] objects;
            [SerializeField] private GameObject endAnimation;
            [SerializeField] private GameObject appearanceOfCup;
            [SerializeField] private GameObject fireworks;
            [SerializeField] private GameObject[] insect;
            [SerializeField] private GameObject exitButton;

            public void ToMainMenu() 
            {
                Debug.Log("Particle System playing state: " + activePS.isPlaying);
                if(activePS.isPlaying)
                {
                    deactivateGameObject();
                    activateAnimation();
                    activateExitButton();
                }
            }

            public void deactivateGameObject()
            {
                Debug.Log("Deactivate all game object");
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(false);
                }
            }

            public void activateAnimation()
            {
                Debug.Log("Activating the final animation");
                insect[1].SetActive(false);
                endAnimation.SetActive(true);
                appearanceOfCup.SetActive(true);
                fireworks.SetActive(true);
                insect[0].SetActive(true);
            }

            public void activateExitButton()
            {
                Debug.Log("Activating the exit button");
                exitButton.SetActive(true);
            }
        }
    }
}
