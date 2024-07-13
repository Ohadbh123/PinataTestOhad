using DG.Tweening;
using UnityEngine;

namespace Gameplay.Utils
{
    //simple tween for roatation aniamtion
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Start()
        {
            transform.DORotate(new Vector3(0, 0, 360), _speed, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative(true);
        }
    }
}