using DG.Tweening;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay
{
    public enum MovementType
    {
        None,
        Rotate90,
        Rotate180,
    }
    
    [CreateAssetMenu(menuName = "Config/Cannon Config File")]
    public class CannonConfig : ScriptableObject
    {
        public MovementType OnHitMovementType;
        public ContinuousMovementConfig ContinuousMovementConfig;
        public GameObject ProjectilePrefab;
        public bool CanShootOnStartup;
        public float MovementSpeed;
        public float CooldownTime;

        public void TurnCannonOnHit(Transform cannonTransform)
        {
            switch (OnHitMovementType)
            {
                case MovementType.Rotate90:
                    var newPosRot90 = new Vector3(0, 0, 90);
                    cannonTransform.DORotate(newPosRot90, MovementSpeed, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine);
                    break;
                case MovementType.Rotate180:
                    var newPosRot180 = new Vector3(0, 0, 180);
                    cannonTransform.DORotate(newPosRot180, MovementSpeed, RotateMode.FastBeyond360)
                        .SetEase(Ease.InOutSine);
                    break;
            }
        }
    }
}