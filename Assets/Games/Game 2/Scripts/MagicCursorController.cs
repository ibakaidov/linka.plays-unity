using UnityEngine;

namespace Shell
{
    namespace Game2
    {
        public class MagicCursorController : MonoBehaviour
        {
            [SerializeField] private float shiftX = 0;
            [SerializeField] private float shiftY = 0;

            private RectTransform cursor;
            void Start()
            {
                cursor = GetComponent<RectTransform>();
            }

            void Update()
            {
                Vector2 c = ControlMaster.mode == ControlMode.Sight ? SightMaster.Point : Input.mousePosition;
                cursor.transform.position = new Vector3(c.x + shiftX, c.y + shiftY, Camera.main.nearClipPlane);
            }
        }
    }
}
