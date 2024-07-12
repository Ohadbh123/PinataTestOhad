using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private LevelButtonView[] _levelsButtons;
        [SerializeField] private GameObject[] _levels;
        [SerializeField] private Transform _levelHolder;
        [SerializeField] private float _animationDelay = 0.5f;

        private int _currentLevelIndex;
        private LevelManager _currentLevel;

        private void Start()
        {
            SetupButtonListeners();
            transform.localScale = Vector3.zero;
            _closeButton.onClick.AddListener(CloseLevelSelector);
        }

        private void CloseLevelSelector()
        {
            transform.DOScale(Vector3.zero, _animationDelay/2);
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
                Destroy(_currentLevel.gameObject);
                _currentLevel = null;
            }
        }

        public void LoadLevelSelector()
        {
            KillCurrentLevel();
            transform.DOScale(Vector3.one, _animationDelay);
        }

        private void SetupButtonListeners()
        {
            for (var i = 0; i < _levelsButtons.Length; i++)
            {
                _levelsButtons[i].SetLevelIndex(i + 1);
                _levelsButtons[i].AddButtonListener(LoadLevel , i);
            }
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

