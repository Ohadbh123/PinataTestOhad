using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameManager : MonoBehaviour
    { 
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _levelCompletePopup;
        [SerializeField] private LevelSelector _levelSelector;
        [SerializeField] private Button _PlayButton;

        private static GameManager instance;
        private int _starsAmount;

        #region Singleton

        public static GameManager Instance
        {
            get
            {
                if (instance != null) return instance;
                
                instance = FindObjectOfType<GameManager>();

                if (instance != null) return instance;
                
                var singletonObject = new GameObject("GameManager");
                instance = singletonObject.AddComponent<GameManager>();

                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        private void Start()
        {
            _PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            _PlayButton.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
            {
                _PlayButton.gameObject.SetActive(false);
            });
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

        private IEnumerator LoadLevelCompletedPopup(int coinCount)
        {
            yield return new WaitForSeconds(1.5f);

            var levelCompletePopup = Instantiate(_levelCompletePopup, _canvas).GetComponent<LevelCompletePopupView>();
            levelCompletePopup.SetStartsAmount(coinCount);
        }
    }
}
