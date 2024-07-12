using DG.Tweening;
using UnityEngine;

namespace Gameplay.Utils
{
    public enum ContinuousMovementType
    {
        None,
        Rotate90,
        Rotate180,
        YoYo
    }
    
    [CreateAssetMenu(menuName = "Config/Continuous Movement Config File")]

    public class ContinuousMovementConfig : ScriptableObject
    {
        public ContinuousMovementType startupContinuousMovementType;
        public float MovementMaxPosition;

        public void SetCannonContinuousMovement(Transform cannonTransform, float speed)
        {
            switch (startupContinuousMovementType)
            {
                case ContinuousMovementType.YoYo:
                    var newPosYoyo = new Vector2(MovementMaxPosition, cannonTransform.position.y);
                    cannonTransform.DOMove(newPosYoyo, speed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                    break;
                case ContinuousMovementType.Rotate90:
                    var newPosRot90 = new Vector3(0, 0, 90);
                    cannonTransform.DORotate(newPosRot90, speed, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);
                    break;
                case ContinuousMovementType.Rotate180:
                    var newPosRot180 = new Vector3(0, 0, 180);
                    cannonTransform.DORotate(newPosRot180, speed, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);
                    break;
            }
        }
    }
}