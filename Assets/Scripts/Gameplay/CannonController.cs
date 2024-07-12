using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay
{
    public class CannonController : MonoBehaviour, IInteractable
    {
        [SerializeField] private CannonConfig _cannonConfig;
        [SerializeField] private Transform _aimTransform;
        [SerializeField] private Collider2D _collider2D;

        private bool _isAvailableToShoot;

        void Start()
        {
            _isAvailableToShoot = _cannonConfig.CanShootOnStartup;

            // is Continuous movement required
            if (_cannonConfig.ContinuousMovementConfig != null)
            {
                _cannonConfig.ContinuousMovementConfig.SetCannonContinuousMovement(transform, _cannonConfig.MovementSpeed);
            }
        }

        private void OnMouseDown()
        {
            if (!_isAvailableToShoot) return;

            _collider2D.enabled = false;
            StartCoroutine("ShootProjectile");
        }

        private IEnumerator ShootProjectile()
        {
            var projectileController = 
                Instantiate(_cannonConfig.ProjectilePrefab, _aimTransform.position,Quaternion.identity)
                    .GetComponent<ProjectileController>();
            
            projectileController.OnProjectileInteractibleHit?.AddListener(DeactivateCannon);
            
            yield return new WaitForSeconds(_cannonConfig.CooldownTime);
            
            _collider2D.enabled = _isAvailableToShoot;
        }

        private void DeactivateCannon()
        {
            _isAvailableToShoot = false;
            _collider2D.enabled = false;
        }

        private void ResetCannon()
        {
            _isAvailableToShoot = true;
            _collider2D.enabled = true;
        }

        public void Interact(ProjectileController projectileController)
        {
            projectileController.DestroyProjectile();
            _cannonConfig.TurnCannonOnHit(transform);
            ResetCannon();
        }
    }
}
