using DG.Tweening;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.Data
{
    public enum MovementType
    {
        None,
        Rotate90,
        Rotate180,
        Rotate360
    }
    
    [CreateAssetMenu(menuName = "Config/Cannon Config File")]
    public class CannonConfig : ScriptableObject
    {
        public MovementType OnHitMovementType;
        public ContinuousMovementConfig ContinuousMovementConfig;
        public GameObject ProjectilePrefab;
        public bool CanShootOnStartup;
        public float MovementSpeed;
        public float TurnMovementSpeed;
        public float CooldownTime;

        public void TurnCannonOnHit(Transform cannonTransform)
        {
            switch (OnHitMovementType)
            {
                case MovementType.Rotate90:
                    var newPosRot90 = new Vector3(0, 0, 90);
                    cannonTransform.DORotate(newPosRot90, TurnMovementSpeed, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine);
                    break;
                case MovementType.Rotate180:
                    var newPosRot180 = new Vector3(0, 0, 180);
                    cannonTransform.DORotate(newPosRot180, TurnMovementSpeed, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine);
                    break;
                case MovementType.Rotate360:
                    var newPosRot360 = new Vector3(0, 0, 360);
                    cannonTransform.DORotate(newPosRot360, TurnMovementSpeed, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine);
                    break;
            }
        }
    }
}