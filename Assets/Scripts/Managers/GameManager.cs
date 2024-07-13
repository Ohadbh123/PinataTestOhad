using System.Collections;
using DG.Tweening;
using Infrastructure;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    { 
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _levelCompletePopup;
        [SerializeField] private LevelSelector _levelSelector;
        [SerializeField] private Button _playButton;
        [SerializeField] private Transform _mainContent;

        private int _starsAmount;
        
        private void Start()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            LoadMainContent(false);
            LoadLevelSelector();
        }

        private void UpdateStarsAmount(int amount)
        {
            _starsAmount += amount;
        }

        public void OnLevelCompleted(int coinCount)
        {
            UpdateStarsAmount(coinCount);
            StartCoroutine(LoadLevelCompletedPopup(coinCount));
        }

        public void RestartLevel()
        {
            _levelSelector.RestartLevel();
        }

        public void LoadNextLevel()
        {
            _levelSelector.LoadNextLevel();
        }

        public void LoadLevelSelector()
        {
            _levelSelector.LoadLevelSelector();
        }

        public void LoadMainContent(bool load)
        {
            _mainContent.transform.DOScale(load? Vector3.one : Vector3.zero, 0.5f);
        }

        private IEnumerator LoadLevelCompletedPopup(int coinCount)
        {
            yield return new WaitForSeconds(1.5f);

            var levelCompletePopup = Instantiate(_levelCompletePopup, _canvas).GetComponent<LevelCompletePopupView>();
            levelCompletePopup.SetStartsAmount(coinCount);
        }
    }
}
