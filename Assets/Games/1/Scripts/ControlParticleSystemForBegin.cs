using System.Runtime.InteropServices;
using System;

using UnityEngine;
using UnityEngine.UI;

namespace Shell 
{
    namespace Game1
    {
        public class ControlParticleSystemForBegin : MonoBehaviour
        {
            private SpriteRenderer cursor;
            [SerializeField] private ParticleSystem ps;
            [SerializeField] private Camera partialCam;
            
            private void OnEnable()
            {
                ps.Play();
            }

            private void OnDisable()
            {
                ps.Stop();
            }

        }
    }
}
