using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelCompletePopupView : MonoBehaviour
    {
        [SerializeField] private RectTransform [] _starsHolders;
        [SerializeField] private RectTransform _youWinText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private float _animationDelay = 0.5f;
        
        private Sequence _tweenSequence;

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, _animationDelay);
            _closeButton.onClick.AddListener(OnCloseButtonPressed);
            _nextLevelButton.onClick.AddListener(LoadNextLevel);
        }

        public void SetStartsAmount(int starsAmount)
        {
            _tweenSequence = DOTween.Sequence();

            for (var i = 0; i < starsAmount; i++)
            {
                _tweenSequence
                    .Append(_starsHolders[i]
                    .DOScale(Vector3.one, _animationDelay))
                    .SetDelay(_animationDelay);
            }
            
            _tweenSequence.Append
                    (_youWinText.DOScale(Vector3.one, _animationDelay))
                .SetDelay(_animationDelay);

            _tweenSequence.Append
                (_nextLevelButton.transform.DOScale(Vector3.one / 2, _animationDelay));
        }

        private void OnCloseButtonPressed()
        {
            transform.DOScale(Vector3.zero, _animationDelay).OnComplete(() =>
            {
                GameManager.Instance.LoadLevelSelector();
                Destroy(gameObject);
            });
        }

        private void LoadNextLevel()
        {
            transform.DOScale(Vector3.zero, _animationDelay).OnComplete(() =>
            {
                GameManager.Instance.LoadNextLevel();
                Destroy(gameObject);
            });
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
            _nextLevelButton.onClick.RemoveListener(LoadNextLevel);
        }
    }
}