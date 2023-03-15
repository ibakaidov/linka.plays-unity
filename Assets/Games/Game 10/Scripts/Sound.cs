using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shell
{
    namespace Game10
    {
        public class Sound : MonoBehaviour
        {
            [SerializeField] private GameObject game10;
            [SerializeField] private AudioSource sound;

            void Start()
            {
                if (game10.activeSelf) {
                    sound.Play();
                }
            }

            // Update is called once per frame
            void Update()
            {
                if (game10.activeSelf && !sound.isPlaying) {
                    sound.Play();
                }

                if (!game10.activeSelf) {
                    sound.Stop();
                }
            }
        }
    }
}
