using DG.Tweening;
using UnityEngine;

namespace Gameplay.Utils
{
    public class SimpleIdleAnimation : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _size = Vector3.one/2;
        private void Start()
        {
            transform.DOScale(_size, _speed)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetRelative(true);
        }
    }
}