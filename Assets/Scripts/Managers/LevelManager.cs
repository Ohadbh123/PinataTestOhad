using Gameplay.Data;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Controllers and Data")] 
        [SerializeField] private PinataController _pinataController;
        [SerializeField] private CoinController[] _coinControllers;
        [SerializeField] private LevelData _levelData;

        [Header("UI and Sound")] 
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _introBarText;
        [SerializeField] private AudioSource _levelIntroSound;

        private int _coinCount;

        private void Start()
        {
            //set level name intro
            _introBarText.text = gameObject.name.Substring(0, 7);

            //button listeners
            _pinataController.OnPinataAchieved.AddListener(OnLevelCompleted);
            _restartButton.onClick.AddListener(RestartLevel);
            _exitButton.onClick.AddListener(ExitLevel);

            AudioManager.Instance.PlayWithDelay(.7f, _levelIntroSound);

            //collectibles listeners
            foreach (var controller in _coinControllers)
            {
                controller.OnStarAchieved.AddListener(OnCoinAchieved);
            }
        }
        
        private void OnCoinAchieved()
        {
            _coinCount++;
        }

        //level handling functions
        public void KillLevel()
        {
            DestroyImmediate(gameObject);
        }

        public void UpdateLevelData(int coinCollected)
        {
            _levelData.UpdateStarsAmount(coinCollected);
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

        //remove listeners
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartLevel);
            _exitButton.onClick.RemoveListener(ExitLevel);
            _pinataController.OnPinataAchieved.RemoveListener(OnLevelCompleted);
        }
    }
}