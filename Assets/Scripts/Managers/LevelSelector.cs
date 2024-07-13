using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LevelButtonView[] _levelsButtons;
        [SerializeField] private GameObject[] _levels;
        [SerializeField] private Transform _levelHolder;
        [SerializeField] private float _animationDelay = 0.1f;

        private int _currentLevelIndex;
        private LevelManager _currentLevel;
        private Sequence _tweenSequence;

        private void Start()
        {
            SetupButtonListeners();
            transform.localScale = Vector3.zero;
            _closeButton.onClick.AddListener(LoadMainScreen);
        }

        private void CloseLevelSelector()
        {
            transform.DOScale(Vector3.zero, _animationDelay);
        }

        private void LoadLevel (int levelIndex)
        {
            KillCurrentLevel();
            CloseLevelSelector();
            _currentLevel = Instantiate(_levels[levelIndex].gameObject, _levelHolder).GetComponent<LevelManager>();
            _currentLevelIndex = levelIndex;
        }

        public void LoadNextLevel()
        {
            KillCurrentLevel();
            _currentLevelIndex = _currentLevelIndex + 1 <= _levels.Length ? _currentLevelIndex++ : 0;
            _currentLevel = Instantiate(_levels[_currentLevelIndex].gameObject, _levelHolder).GetComponent<LevelManager>();
        }

        private void KillCurrentLevel()
        {
            if (_currentLevel != null)
            {
                _currentLevel.KillLevel();
                _currentLevel = null;
            }
        }
        
        public void RestartLevel()
        {
            KillCurrentLevel();
            _currentLevel = Instantiate(_levels[_currentLevelIndex].gameObject, _levelHolder).GetComponent<LevelManager>();
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

        private void SetupButtonListeners()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].SetLevelIndex(i + 1);
                _levelsButtons[i].AddButtonListener(LoadLevel , i);
            }
        }
        
        private void LoadMainScreen()
        {
            CloseLevelSelector();
            GameManager.Instance.LoadMainContent(true);
        }
        
        private void OnDestroy()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].RemoveButtonListener(LoadLevel , i);
            }
            _closeButton.onClick.RemoveListener(CloseLevelSelector);
        }

      
    }
}

