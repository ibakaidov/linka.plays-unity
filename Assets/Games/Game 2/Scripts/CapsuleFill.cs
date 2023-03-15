using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game2
    {
        public class CapsuleFill : MonoBehaviour
        {
            [SerializeField] private Image image;

            public float Amount
            {
                set
                {
                    image.fillAmount = value;
                    Vector3 eu = image.transform.eulerAngles;
                    eu.z = value;
                    image.transform.eulerAngles = eu;
                }
            }
        }

    }
}
