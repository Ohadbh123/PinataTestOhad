using Gameplay.Interfaces;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay
{
    public class ObstacleController : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _damageVfx;
        [SerializeField] private ContinuousMovementConfig _continuousMovementConfig;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _movementMaxPosition;
        [SerializeField] private bool _isDestroyable;

        private void Start()
        {
            if (_continuousMovementConfig != null)
            {
                _continuousMovementConfig.SetCannonContinuousMovement(transform, _movementSpeed, _movementMaxPosition);
            }
        }

        public void TakeDamage(Transform collisionPosition)
        {
            Instantiate(_damageVfx, collisionPosition.position, Quaternion.identity);
            if (!_isDestroyable) return;
            Destroy(gameObject);
        }
    }
}