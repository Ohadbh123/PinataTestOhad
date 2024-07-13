using System;
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

        private void Start()
        {
            if (_continuousMovementConfig != null)
            {
                _continuousMovementConfig.SetCannonContinuousMovement(transform, _movementSpeed, _movementMaxPosition);
            }
        }

        public void TakeDamage()
        {
            Instantiate(_damageVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}