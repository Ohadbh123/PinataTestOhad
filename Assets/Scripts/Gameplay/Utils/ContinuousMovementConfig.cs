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
        public bool _xAxis;

        public void SetCannonContinuousMovement(Transform ObjectTransform, float speed, float movementMaxPosition)
        {
            switch (startupContinuousMovementType)
            {
                case ContinuousMovementType.YoYo:
                    var newPosYoyo = _xAxis
                        ? new Vector2(ObjectTransform.position.x, movementMaxPosition)
                        : new Vector2(movementMaxPosition, ObjectTransform.position.y);

                    
                    ObjectTransform.DOMove(newPosYoyo, speed).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
                    break;
                case ContinuousMovementType.Rotate90:
                    var newPosRot90 = new Vector3(0, 0, 90);
                    ObjectTransform.DORotate(newPosRot90, speed, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);
                    break;
                case ContinuousMovementType.Rotate180:
                    var newPosRot180 = new Vector3(0, 0, 180);
                    ObjectTransform.DORotate(newPosRot180, speed, RotateMode.FastBeyond360).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Incremental);
                    break;
            }
        }
    }
}