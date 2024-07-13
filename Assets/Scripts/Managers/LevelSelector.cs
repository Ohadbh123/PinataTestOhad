using System;
using DG.Tweening;
using Gameplay;
using Gameplay.Data;
using Managers;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LevelButtonView[] _levelsButtons;
        [SerializeField] private Level[] _levels;
        [SerializeField] private Transform _levelHolder;
        [SerializeField] private float _animationDelay = 0.1f;
        [SerializeField] private TMP_Text _starsAmountText;

        private int _currentLevelIndex;
        private LevelManager _currentLevelManager;
        private Sequence _tweenSequence;

        private void Start()
        {
            UpdateStarsAmount();
            SetupButtons();
            transform.localScale = Vector3.zero;
            _closeButton.onClick.AddListener(LoadMainScreen);
        }

        private void CloseLevelSelector()
        {
            transform.DOScale(Vector3.zero, _animationDelay);
        }

        private void LoadLevel (int levelIndex)
        {
            AudioManager.Instance.PlayButtonSound();
            KillCurrentLevel();
            CloseLevelSelector();
            InstantiateLevel(levelIndex);
            _currentLevelIndex = levelIndex;
            GameManager.Instance.UpdateGameState(GameState.Level);
        }

        private void InstantiateLevel(int index)
        {
            var levelPrefab = Instantiate(_levels[index].LevelPrefab, _levelHolder).GetComponent<LevelManager>();
            _currentLevelManager = levelPrefab.GetComponent<LevelManager>();
        }

        public void LoadNextLevel()
        {
            KillCurrentLevel();
            
            if (_currentLevelIndex + 1 <= _levels.Length -1)
            {
                _currentLevelIndex++;
            }
            else
            {
                _currentLevelIndex = 0;
            }
            
            InstantiateLevel(_currentLevelIndex);
            GameManager.Instance.UpdateGameState(GameState.Level);
        }

        private void KillCurrentLevel()
        {
            if (_currentLevelManager != null)
            {
                _currentLevelManager.KillLevel();
                _currentLevelManager = null;
            }
        }
        
        public void RestartLevel()
        {
            KillCurrentLevel();
            InstantiateLevel(_currentLevelIndex);
        }

        public void LoadLevelSelector()
        {
            KillCurrentLevel();
            
            _tweenSequence = DOTween.Sequence();
            _tweenSequence.SetDelay(_animationDelay);
            transform.DOScale(Vector3.one, _animationDelay);
            
            foreach (var button in _levelsButtons)
            { 
                _tweenSequence.Append
                    (button.transform.DOScale(Vector3.one, _animationDelay));
            }
            
            _tweenSequence.Append(_closeButton.transform.DOScale(Vector3.one, _animationDelay));
        }

        private void SetupButtons()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].SetLevelStatus(_levels[i].LevelData.GetStarsAmount());
                _levelsButtons[i].SetLevelIndex(i + 1);
                _levelsButtons[i].AddButtonListener(LoadLevel , i);
            }
        }
        
        private void LoadMainScreen()
        {
            CloseLevelSelector();
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.LoadMainContent(true);
            GameManager.Instance.UpdateGameState(GameState.MainScreen);
        }
        
        private void OnDestroy()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].RemoveButtonListener(LoadLevel , i);
            }
            _closeButton.onClick.RemoveListener(CloseLevelSelector);
        }

        private int GetTotalStarsAmount()
        {
            return _levels.Length * 3;
        }

        public void UpdateLevelCompleted(int coins)
        {
            _currentLevelManager.UpdateLevelData(coins);
            _levelsButtons[_currentLevelIndex].SetLevelStatus(coins);
        }
        
        public void UpdateStarsAmount()
        {
            var starsAmount = 0;

            for (var i = 0; i < _levels.Length; i++)
            {
                starsAmount += _levels[i].LevelData.GetStarsAmount();
            }
            
            _starsAmountText.text = starsAmount + "/" + GetTotalStarsAmount();
        }

        public void ResetProgress()
        {
            for (var i = 0; i < _levels.Length; i++)
            {
                _levels[i].LevelData.ResetProgress();
            }

            foreach (var button in _levelsButtons)
            {
                button.ResetButton();
            }
            UpdateStarsAmount();
        }
    }
}

[Serializable]
internal struct Level
{
    public LevelData LevelData;
    public GameObject LevelPrefab;
}

