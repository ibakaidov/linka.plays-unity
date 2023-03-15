using UnityEngine;

namespace Shell
{
    namespace Game2
    {
        public class Sound : MonoBehaviour
        {
            private AudioSource sound;

            public void PlaySound()
            {
                sound = GetComponent<AudioSource>();
                sound.Play();
            }

        }
    }
}
