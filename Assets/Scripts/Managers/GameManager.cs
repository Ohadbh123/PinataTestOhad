using System.Collections;
using DG.Tweening;
using Infrastructure;
using TMPro;
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
        [SerializeField] private AudioSource _mainThemeSound;
        [SerializeField] private AudioSource _levelThemeSound;
        [SerializeField] private TMP_Text _starsAmountText;

        private int _starsAmount;
        private GameState _gameState = GameState.Startup;

        private void Start()
        {
            UpdateStarsAmount(0);
            LoadMainContent(true);
            SwitchThemeMusic(_gameState);
            UpdateGameState(GameState.MainScreen);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            AudioManager.Instance.PlayButtonSound();
            LoadMainContent(false);
            LoadLevelSelector();
        }

        private void UpdateStarsAmount(int amount)
        {
            _starsAmount += amount;
            _starsAmountText.text = _starsAmount + "/" + _levelSelector.GetTotalStarsAmount();
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

        public void LoadMainContent(bool shouldLoadMainContent)
        {
            _mainContent.transform.DOScale(shouldLoadMainContent? Vector3.one : Vector3.zero, 0.5f);
        }

        public void UpdateGameState(GameState gameState)
        {
            SwitchThemeMusic(gameState);
            _gameState = gameState;
        }

        private IEnumerator LoadLevelCompletedPopup(int coinCount)
        {
            yield return new WaitForSeconds(1.5f);

            var levelCompletePopup = Instantiate(_levelCompletePopup, _canvas).GetComponent<LevelCompletePopupView>();
            levelCompletePopup.SetStartsAmount(coinCount);
        }

        private void SwitchThemeMusic(GameState gameState)
        {
            if (_gameState == gameState) return;
            
            switch (gameState)
            {
                case GameState.MainScreen:
                    AudioManager.Instance.Play(_mainThemeSound);
                    AudioManager.Instance.Stop(_levelThemeSound);
                    break;
                case GameState.Level:
                    AudioManager.Instance.PlayWithDelay(2f,_levelThemeSound);
                    AudioManager.Instance.Stop(_mainThemeSound);
                    break;
            }

        }
    }
}

public enum GameState
{
    Startup,
    MainScreen,
    Level
}
