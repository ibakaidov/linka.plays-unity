using UnityEngine;

namespace Shell
{
    namespace Game2
    {
        public class Game2 : MonoBehaviour
        {
            [SerializeField] private Color color;
            [SerializeField] private ParticleSystem ps;
            [SerializeField] private GameObject lvlChoosing;

            [SerializeField] private GameObject[] darkBackgroundBtn;

            [SerializeField] private GameObject[] lightBackgroundBtn;

            public void Init()
            {
                lvlChoosing.SetActive(true);
                SetDarkColor();
            }

            public void SetDefault()
            {
                ChangeColor();
            }

            private void ChangeColor()
            {
                Camera.main.backgroundColor = color;
            }

            private void SetActiveBtns(GameObject[] obj, bool flag) {
                obj[0].SetActive(flag);
                obj[1].SetActive(!flag);
            }

            public void SetDarkColor()
            {
                ChangeColor();
                SetActiveBtns(darkBackgroundBtn, true);
                SetActiveBtns(lightBackgroundBtn, false);
            }

            public void SetLightColor() {
                ChangeColor();
                SetActiveBtns(lightBackgroundBtn, true);
                SetActiveBtns(darkBackgroundBtn, false);
            }
        }
    }
}
