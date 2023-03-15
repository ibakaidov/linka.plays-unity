using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOnDisable : MonoBehaviour
{
    // все уровни
    [SerializeField] private GameObject lvls;

    // трейлы
    [SerializeField] private ParticleSystem[] psToStop;
    [SerializeField] private GameObject[] trailCursorsToStop;

    void OnDisable()
    {
        lvls.SetActive(false);
        foreach (ParticleSystem ps in psToStop)
        {
            ps.Stop();
        }

        foreach(GameObject gm in trailCursorsToStop) {
            gm.SetActive(false);
        }
    }
}
