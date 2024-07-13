using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioManager))]
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        const float FADE_TIME = 2f;
        private float _fading;
        private AudioSource _current;
        private float _maxVolume = 1;
        [SerializeField] private AudioSource _uiButtonAudio;

        public void Play(AudioSource audioSource)
        {         
            _current = audioSource;
            _maxVolume = audioSource.volume;
            _current.clip = audioSource.clip;
            _current.loop = false; 
            _current.volume = 0;
            _current.bypassListenerEffects = true;
            _current.Play();

            _fading = 0.001f;
        }

        public void PlayWithDelay(float delay, AudioSource audioSource)
        {
            StartCoroutine(PlaySoundWithDelay(delay, audioSource));
        }
        
        private IEnumerator PlaySoundWithDelay(float delay, AudioSource audioSource)
        {
            yield return new WaitForSeconds(delay);
            Play(audioSource);
        }

        private void Update()
        {
            HandleCrossFade();
        }

        private void HandleCrossFade()
        {
            if (_fading <= 0f) return;

            _fading += Time.deltaTime;

            var fraction = Mathf.Clamp01(_fading / FADE_TIME);

            //fade
            var logFraction = ToLogarithmicFraction(fraction);

            if (_current) _current.volume = logFraction;

            if (!(fraction >= _maxVolume)) return;
            
            _fading = 0.0f;
        }

        public void Stop(AudioSource audioSource)
        {
            audioSource.Stop();
        }

        public void PlayButtonSound()
        {
            _uiButtonAudio.Play();
        }

        private float ToLogarithmicFraction(float fraction) 
        {
            return Mathf.Log10(1 + 9 * fraction) / Mathf.Log10(10);
        }
    }
}
