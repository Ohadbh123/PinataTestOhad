using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private PinataController _pinataController;
        [SerializeField] private CoinController [] _coinControllers;
        [SerializeField] private TMP_Text _introBarText;
        [SerializeField] private AudioSource _levelIntroSound;

        private int _coinCount;

        private void Start()
        {
            _introBarText.text = gameObject.name.Substring(0, 7);
            
            _pinataController.OnPinataAchieved.AddListener(OnLevelCompleted);
            _restartButton.onClick.AddListener(RestartLevel);
            _exitButton.onClick.AddListener(ExitLevel);
            
            AudioManager.Instance.PlayWithDelay(.7f,_levelIntroSound);
            
            foreach (var controller in _coinControllers)
            {
                controller.OnStarAchieved.AddListener(OnCoinAchieved);
            }
        }

        private void ExitLevel()
        {
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.LoadLevelSelector();
        }

        private void RestartLevel()
        {
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.RestartLevel();
        }

        private void OnLevelCompleted()
        {
            GameManager.Instance.OnLevelCompleted(_coinCount);
        }
    
        private void OnCoinAchieved()
        {
            _coinCount++;
        }

        public void KillLevel()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartLevel);
            _exitButton.onClick.RemoveListener(ExitLevel);
            _pinataController.OnPinataAchieved.RemoveListener(OnLevelCompleted);
        }
    }
}
