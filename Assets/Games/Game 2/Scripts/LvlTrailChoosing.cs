using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shell
{
    namespace Game2
    {
        public class LvlTrailChoosing : MonoBehaviour
        {
            // все уровни
            [SerializeField] private GameObject lvls;
            // выбор уровней
            [SerializeField] private GameObject lvlsChoosing;
            // кнопка назад к выбору уровня
            [SerializeField] private GameObject exitToLevelsBtn;
            // какие уровни нужно показать
            [SerializeField] private GameObject[] lvlsToShow;
            // обычный курсор
            [SerializeField] private GameObject ordinaryCursor;

            // трейлы
            [SerializeField] private ParticleSystem[] psToStop;
            [SerializeField] private GameObject[] trailCursorsToStop;


            [SerializeField] private GameObject rawTrail;
            [SerializeField] private Texture texture;

            [SerializeField] private GameObject selectedTrailCursor;
            [SerializeField] private ParticleSystem selectedPs;

            private void Init()
            {
                Cursor.visible = false;
                SetVisibility(true);
            }

            private void SetTexture(bool flag, Texture texture)
            {
                rawTrail.SetActive(flag);
                rawTrail.GetComponent<RawImage>().texture = texture;
            }

            private void SetPs()
            {
                for (int i = 0; i < psToStop.Length; ++i)
                {
                    psToStop[i].Clear();
                    psToStop[i].Pause();
                }
            }

            // переход с уровня на выбор
            public void SetVisibility(bool flag)
            {
                lvls.SetActive(flag);

                lvlsChoosing.SetActive(!flag);

                foreach (GameObject obj in lvlsToShow)
                {
                    obj.SetActive(flag);
                }

                ordinaryCursor.SetActive(flag);

                exitToLevelsBtn.SetActive(flag);
            }

            public void BackToLvlChoosing()
            {
                Cursor.visible = true;
                SetVisibility(false);
                for (int i = 0; i < psToStop.Length; ++i)
                {
                    psToStop[i].Clear();
                    psToStop[i].time = 0f;
                    psToStop[i].Pause();
                    trailCursorsToStop[i].SetActive(false);
                }
                SetTexture(false, null);
            }

            public void StartTrail()
            {
                ordinaryCursor.SetActive(false);
                selectedPs.Play();
                selectedTrailCursor.SetActive(true);
                SetTexture(true, texture);
                foreach (GameObject obj in lvlsToShow)
                {
                    obj.SetActive(false);
                }
            }

            public void EasyLevel()
            {
                Init();
                StartTrail();
            }

            public void MediumLevel()
            {
                Init();
            }

            public void HardLevel()
            {
                Init();
            }
        }

    }
}
