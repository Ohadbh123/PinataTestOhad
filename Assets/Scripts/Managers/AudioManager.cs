using System.Collections;
using Infrastructure;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioManager))]
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        const float FADE_TIME = 2f;
        
        [SerializeField] private AudioSource _uiButtonAudio;

        private float _fading;
        private AudioSource _current;
        private float _maxVolume = 1;

        public void Play(AudioSource audioSource)
        {         
            _current = audioSource;
            _maxVolume = audioSource.volume;
            _current.clip = audioSource.clip;
            
            //Set everything to zero before fading in
            _current.loop = false; 
            _current.volume = 0;
            _current.bypassListenerEffects = true;
            _current.Play();

            _fading = 0.001f;
        }

        public void Stop(AudioSource audioSource)
        {
            audioSource.Stop();
        }

        //handle all UI sound
        public void PlayButtonSound()
        {
            _uiButtonAudio.Play();
        }
        
        private void Update()
        {
            HandleCrossFade();
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

        
        //Fade volume intro
        private void HandleCrossFade()
        {
            if (_fading <= 0f) return;

            _fading += Time.deltaTime;

            var fraction = Mathf.Clamp01(_fading / FADE_TIME);

            var logFraction = ToLogarithmicFraction(fraction);

            if (_current) _current.volume = logFraction;

            if (!(fraction >= _maxVolume)) return;
            
            _fading = 0.0f;
        }

        private float ToLogarithmicFraction(float fraction) 
        {
            return Mathf.Log10(1 + 9 * fraction) / Mathf.Log10(10);
        }
    }
}
