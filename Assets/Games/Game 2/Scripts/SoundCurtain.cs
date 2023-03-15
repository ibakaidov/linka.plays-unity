using UnityEngine;

public class Sound : StateMachineBehaviour
{
    [SerializeField] private AudioSource sound;
    void OnStateEnter() {
        sound.Play();
    }
}
