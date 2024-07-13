using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPopupView : PopupBaseView
    {
        [SerializeField] private Button _resetProgressButton;
        [SerializeField] private Toggle _soundToggle;

        protected override void Start()
        {
            base.Start();

            _resetProgressButton.onClick.AddListener(ResetProgress);
            _soundToggle.onValueChanged.AddListener(delegate {
                ToggleAudio(_soundToggle);
            });
        }

        private void ToggleAudio(Toggle toggle)
        {
            AudioListener.volume = toggle.isOn? 1 : 0;
        }

        private void ResetProgress()
        {
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.ResetProgress();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _resetProgressButton.onClick.RemoveListener(ResetProgress);
            _soundToggle.onValueChanged.RemoveListener(delegate {
                ToggleAudio(_soundToggle);
            });
        }
    }
}