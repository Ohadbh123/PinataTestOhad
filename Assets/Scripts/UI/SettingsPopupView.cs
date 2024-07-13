using Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SettingsPopupView : PopupBaseView
    {
        [SerializeField] private Button _resetProgressButton;

        protected override void Start()
        {
            base.Start();
            
            _resetProgressButton.onClick.AddListener(ResetProgress);
        }

        private void ResetProgress()
        {
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.ResetProgress();
        }
    }
}