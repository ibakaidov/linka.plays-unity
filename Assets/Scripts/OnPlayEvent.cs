using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnPlayEvent : MonoBehaviour
{
    public UnityEvent OnPlay;
    public bool DestroyOnPlay = true;
    
    AudioSource audioSource;
    float last = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (audioSource.time != last && !audioSource.isPlaying)
        {
            OnPlay.Invoke();
            if (DestroyOnPlay)
            {
                Destroy(gameObject);
            }
            else {
                // audioSource.Stop();
            }
        }
        last = audioSource.time;
    }
}
