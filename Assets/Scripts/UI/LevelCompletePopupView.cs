using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelCompletePopupView : PopupBaseView
    {
        [SerializeField] private RectTransform[] _starsHolders;
        [SerializeField] private RectTransform _youWinText;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private AudioSource _winSound;

        private Sequence _tweenSequence;

        protected override void Start()
        {
            base.Start();
            AudioManager.Instance.Play(_winSound);
            _nextLevelButton.onClick.AddListener(LoadNextLevel);
        }

        protected override void OnCloseButtonPressed()
        {
            AudioManager.Instance.PlayButtonSound();

            _panelHolder.transform.DOScale(Vector3.zero, _animationDelay).OnComplete(() =>
            {
                GameManager.Instance.LoadLevelSelector();
                Destroy(gameObject);
            });
        }

        //Show how many starts achieved on this level
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

        private void LoadNextLevel()
        {
            AudioManager.Instance.PlayButtonSound();

            _panelHolder.transform.DOScale(Vector3.zero, _animationDelay).OnComplete(() =>
            {
                GameManager.Instance.LoadNextLevel();
                Destroy(gameObject);
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _nextLevelButton.onClick.RemoveListener(LoadNextLevel);
        }
    }
}