using System.Runtime.InteropServices;
using System;

using UnityEngine;
using UnityEngine.UI;

namespace Shell 
{
    namespace Game1
    {
        public class ControlParticleSystem : MonoBehaviour
        {
            private SpriteRenderer cursor;
            [SerializeField] private ParticleSystem ps;
            [SerializeField] private Camera partialCam;
            [SerializeField] private AudioSource audio;
            
            private void OnEnable()
            {
                Debug.Log("Play Particle System and audio");
                cursor = GetComponent<SpriteRenderer>();
                Update();
                ps.Play();
                audio.Play();
            }
            void Update()
            {

                Vector2 c = ControlMaster.mode == ControlMode.Sight ? SightMaster.Point : Input.mousePosition;

                Vector3 world = Camera.main.ScreenToWorldPoint(new Vector3(c.x, c.y, Camera.main.nearClipPlane));
                Debug.Log("Координаты камеры - " + c.x + " " + c.y + " " + Camera.main.nearClipPlane);
                Debug.Log("Координаты мира - " + world.x + " " + world.y + " " + world.z);
                cursor.transform.position = world;
                ps.transform.position = world;

            }

            private void OnDisable()
            {
                Debug.Log("Stop Particle System and audio");
                ps.Stop();
                audio.Stop();
            }

        }
    }
}
