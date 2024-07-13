using System;
using DG.Tweening;
using Gameplay;
using Gameplay.Data;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LevelSelector : MonoBehaviour
    {
        [Header("Level Objects")]
        [SerializeField] private Level[] _levels;
        [SerializeField] private LevelButtonView[] _levelsButtons;
        [SerializeField] private Transform _levelHolder;
        
        [Header("UI")]
        [SerializeField] private TMP_Text _starsAmountText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private float _animationDelay = 0.1f;

        private LevelManager _currentLevelManager;
        private Sequence _tweenSequence;
        private int _currentLevelIndex;

        private void Start()
        {
            //setup stars progress and all levels buttons
            UpdateStarsAmount();
            SetupButtons();
            
            //set screen to zero (loading with tween animation)
            transform.localScale = Vector3.zero;
            _closeButton.onClick.AddListener(LoadMainScreen);
        }
        
        public void LoadNextLevel()
        {
            KillCurrentLevel();

            if (_currentLevelIndex + 1 <= _levels.Length - 1)
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

        public void RestartCurrentLevel()
        {
            KillCurrentLevel();
            InstantiateLevel(_currentLevelIndex);
        }

        //load level selection screen
        public void LoadLevelSelector()
        {
            KillCurrentLevel();

            //tween sequence for button aniamtion
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
        
        //update and reset progress functions
        public void UpdateLevelCompletedData(int coins)
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
        
        //destroys current active level
        private void KillCurrentLevel()
        {
            if (_currentLevelManager == null) return;
            
            _currentLevelManager.KillLevel();
            _currentLevelManager = null;
        }
        
        //close main screen
        private void CloseLevelSelector()
        {
            transform.DOScale(Vector3.zero, _animationDelay);
        }

        //load a specific level and update game state
        private void LoadLevelByIndex(int levelIndex)
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
        
        private void LoadMainScreen()
        {
            CloseLevelSelector();
            
            AudioManager.Instance.PlayButtonSound();
            GameManager.Instance.LoadMainContent(true);
            GameManager.Instance.UpdateGameState(GameState.MainScreen);
        }

        private void SetupButtons()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].SetLevelStatus(_levels[i].LevelData.GetStarsAmount());
                _levelsButtons[i].SetLevelIndex(i + 1);
                _levelsButtons[i].AddButtonListener(LoadLevelByIndex, i);
            }
        }
        
        //get total stars available (level count times 3)
        private int GetTotalStarsAmount()
        {
            return _levels.Length * 3;
        }

        //remove listeners
        private void OnDestroy()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].RemoveButtonListener(LoadLevelByIndex, i);
            }

            _closeButton.onClick.RemoveListener(CloseLevelSelector);
        }
    }
}

[Serializable]
internal struct Level
{
    public LevelData LevelData;
    public GameObject LevelPrefab;
}