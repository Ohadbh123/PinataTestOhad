using System.Collections;
using Gameplay.Data;
using Gameplay.Interfaces;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class CannonController : MonoBehaviour, IInteractable
    {
        private static readonly int _animatorShootTrigger = Animator.StringToHash("Shoot");

        [SerializeField] private Animator _animator;
        [SerializeField] private CannonConfig _cannonConfig;
        [SerializeField] private Transform _aimTransform;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private GameObject _activeCannonMarker;
        [SerializeField] private AudioSource _fireSfx;
        [SerializeField] private float _movementMaxPosition;

        private bool _isAvailableToShoot;
        private bool _wasDeactivated;

        void Start()
        {
            _isAvailableToShoot = _cannonConfig.CanShootOnStartup;
            _activeCannonMarker.SetActive(_isAvailableToShoot);
            GameManager.Instance.InputButtonAddListener(TryShootProjectile);
            // is Continuous movement required
            if (_cannonConfig.ContinuousMovementConfig != null)
            {
                _cannonConfig.ContinuousMovementConfig.SetCannonContinuousMovement(transform, _cannonConfig.MovementSpeed, _movementMaxPosition);
            }
        }

        private void TryShootProjectile()
        {
            if (!_isAvailableToShoot) return;
            
            _animator.SetTrigger(_animatorShootTrigger);
            _collider2D.enabled = false;
            _isAvailableToShoot = false;
            AudioManager.Instance.Play(_fireSfx);

            StartCoroutine("ShootProjectile");
        }

        private IEnumerator ShootProjectile()
        {
            var projectileController = 
                Instantiate(_cannonConfig.ProjectilePrefab, _aimTransform.position,Quaternion.identity)
                    .GetComponent<ProjectileController>();

            projectileController.SetupProjectileDirection(_aimTransform.up);
            projectileController.OnProjectileInteractableHit += DeactivateCannon;
            
            yield return new WaitForSeconds(_cannonConfig.CooldownTime);

            ResetCannon();
        }

        private void DeactivateCannon(ProjectileController projectileController)
        {
            projectileController.OnProjectileInteractableHit -= DeactivateCannon;
            _wasDeactivated = true;
            _isAvailableToShoot = false;
            _collider2D.enabled = true;
            _activeCannonMarker.SetActive(false);
        }
        
        private void ResetCannon()
        {
            if (_wasDeactivated) return;
            
            _isAvailableToShoot = true;
            _collider2D.enabled = true;
            _activeCannonMarker.SetActive(true);
        }

        public void Interact(ProjectileController projectileController)
        {
            _wasDeactivated = false;
            _cannonConfig.TurnCannonOnHit(transform);
            ResetCannon();
            projectileController.DestroyProjectile();
        }

        private void OnDestroy()
        {
            GameManager.Instance.InputButtonRemoveListener(TryShootProjectile);
        }
    }
}
