using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private AudioSource _levelIntroSound;
        [SerializeField] private PinataController _pinataController;
        [SerializeField] private CoinController [] _coinControllers;

        private int _coinCount;

        private void Start()
        {
            _pinataController.OnPinataAchieved.AddListener(OnLevelCompleted);
            _restartButton.onClick.AddListener(RestartLevel);
            AudioManager.Instance.PlayWithDelay(.7f,_levelIntroSound);
            
            foreach (var controller in _coinControllers)
            {
                controller.OnStarAchieved.AddListener(OnCoinAchieved);
            }
        }

        private void RestartLevel()
        {
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
            _restartButton.onClick.AddListener(RestartLevel);
            _pinataController.OnPinataAchieved.RemoveListener(OnLevelCompleted);
        }
    }
}
