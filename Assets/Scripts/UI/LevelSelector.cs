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

        private void Start()
        {
            SetupButtonListeners();
            _closeButton.onClick.AddListener(CloseLevelSelector);
        }

        private void CloseLevelSelector()
        {
            gameObject.SetActive(false);
        }

        private void LoadLevel (int levelIndex)
        {
            var levelManager = Instantiate(_levels[levelIndex].gameObject, _levelHolder).GetComponent<LevelManager>();
            CloseLevelSelector();
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

