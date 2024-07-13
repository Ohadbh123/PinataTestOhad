using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupBaseView : MonoBehaviour 
    {
        [SerializeField] protected RectTransform _panelHolder;
        [SerializeField] protected Button _closeButton;
        [SerializeField] protected float _animationDelay = 0.5f;
        
        protected virtual void Start()
        {
            _panelHolder.localScale = Vector3.zero;
            _panelHolder.DOScale(Vector3.one, _animationDelay);
            _closeButton.onClick.AddListener(OnCloseButtonPressed);
        }
        
        protected virtual void OnCloseButtonPressed()
        {
            AudioManager.Instance.PlayButtonSound();

            _panelHolder.transform.DOScale(Vector3.zero, _animationDelay).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
        
        protected virtual void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonPressed);
        }
    }
}