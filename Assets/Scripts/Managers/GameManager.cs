using System;
using System.Collections;
using DG.Tweening;
using Infrastructure;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    { 
        [SerializeField] private Transform _canvas;
        [SerializeField] private Transform _mainContent;
        [SerializeField] private LevelSelector _levelSelector;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _inputButton;
        [SerializeField] private GameObject _levelCompletePopup;
        [SerializeField] private GameObject _settingsPopup;
        
        [SerializeField] private AudioSource _mainThemeSound;
        [SerializeField] private AudioSource _levelThemeSound;

        private GameState _gameState = GameState.Startup;

        private void Start()
        {
            LoadMainContent(true);
            SwitchThemeMusic(_gameState);
            UpdateGameState(GameState.MainScreen);
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnSettingsButtonClicked()
        {
            AudioManager.Instance.PlayButtonSound();
            Instantiate(_settingsPopup, _canvas);
        }

        private void OnPlayButtonClicked()
        {
            AudioManager.Instance.PlayButtonSound();
            LoadMainContent(false);
            LoadLevelSelector();
        }

        public void OnLevelCompleted(int coinCount)
        {
            _levelSelector.UpdateLevelCompleted(coinCount);
            _levelSelector.UpdateStarsAmount();
            AudioManager.Instance.Stop(_levelThemeSound);
            StartCoroutine(LoadLevelCompletedPopup(coinCount));
        }

        public void RestartLevel()
        {
            _levelSelector.RestartLevel();
        }

        public void LoadNextLevel()
        {
            AudioManager.Instance.PlayWithDelay(2f,_levelThemeSound);
            _levelSelector.LoadNextLevel();
        }

        public void LoadLevelSelector()
        {
            _inputButton.gameObject.SetActive(false);
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
            
            switch (gameState)
            {
                case GameState.MainScreen:
                    _inputButton.gameObject.SetActive(false);
                    break;
                case GameState.Level:
                    _inputButton.gameObject.SetActive(true);
                    break;
            }
        }

        public void InputButtonAddListener(UnityAction action)
        {
            _inputButton.onClick.AddListener(action);
        }
        
        public void InputButtonRemoveListener(UnityAction action)
        {
            _inputButton.onClick.RemoveListener(action);
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
                    _inputButton.gameObject.SetActive(false);
                    AudioManager.Instance.Play(_mainThemeSound);
                    AudioManager.Instance.Stop(_levelThemeSound);
                    break;
                case GameState.Level:
                    _inputButton.gameObject.SetActive(true);
                    AudioManager.Instance.PlayWithDelay(2f,_levelThemeSound);
                    AudioManager.Instance.Stop(_mainThemeSound);
                    break;
            }

        }

        public void ResetProgress()
        {
            _levelSelector.ResetProgress();
        }

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }
    }
}

public enum GameState
{
    Startup,
    MainScreen,
    Level
}
