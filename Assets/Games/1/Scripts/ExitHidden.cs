using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game1
    {
        public class ExitHidden : MonoBehaviour
        {
            [SerializeField] public Button exitButton;

            // Start is called before the first frame update
            void Start()
            {

            }

            // Update is called once per frame
            void Update()
            {
                if (Input.GetKeyUp(KeyCode.Escape)) // Отслеживание нажатия кнопки Escape
                {
                    exitButton.onClick.Invoke();  
                }
            }
        }
    }
}
