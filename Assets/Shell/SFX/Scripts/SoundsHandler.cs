using UnityEngine;

namespace Shell
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundsHandler : MonoBehaviour
    {
        private static SoundsHandler _instance;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }
        
        private void OnEnable() => _instance = this;
        private void OnDisable() => _instance = null;

        public static void PlayLoop(AudioClip clip, float volume)
        {
            _instance._source.volume = volume;
            _instance._source.clip = clip;
            _instance._source.Play();
        }

        public static void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            _instance._source.volume = volume;
            _instance._source.PlayOneShot(clip);
        }

        public static void Stop()
        {
            _instance._source.Stop();
            _instance._source.clip = null;
            _instance._source.volume = 1f;
        }
    }
}
