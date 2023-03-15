using UnityEngine;
using UnityEngine.UIElements;

namespace Shell
{
    namespace Game2
    {
        public class TrailCursor : MonoBehaviour
        {
            private RectTransform cursor;
            [SerializeField] private ParticleSystem ps;

            private Vector3 mousePosition;

            private AudioSource sound;

            void Awake()
            {
                mousePosition = Input.mousePosition;
                sound = GetComponent<AudioSource>();
                InvokeRepeating("PlayTrail", 0f, 0.5f);
            }

            void Start()
            {
                cursor = GetComponent<RectTransform>();
            }

            void Update()
            {
                Vector2 c = ControlMaster.mode == ControlMode.Sight ? SightMaster.Point : Input.mousePosition;
                cursor.transform.position = new Vector3(c.x, c.y, Camera.main.nearClipPlane);
                ps.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(c.x, c.y, Camera.main.nearClipPlane));
            }

            void PlayTrail()
            {
                // play sound
                Vector3 mousePosition2 = ControlMaster.mode == ControlMode.Sight ? SightMaster.Point : Input.mousePosition;
                if (mousePosition.x == mousePosition2.x && mousePosition.y == mousePosition2.y && mousePosition.z == mousePosition2.z)
                {
                    sound.Pause();
                }
                else if (!sound.isPlaying)
                {
                    sound.Play();
                }
                mousePosition = mousePosition2;
            }
        }
    }
}
