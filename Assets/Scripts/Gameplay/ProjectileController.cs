using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private ProjectileConfig _projectileConfig;

        [HideInInspector]
        public UnityEvent OnProjectileInteractibleHit;
        
        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.up * _projectileConfig.ProjectileSpeed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(this);
                OnProjectileInteractibleHit.Invoke();
                return;
            }
            
            if (col.TryGetComponent(out IDamageable damageable))
            {
                damageable.TryTakeDamage(_projectileConfig.ProjectileDamage);
            }
        }

        public void DestroyProjectile()
        {
            OnProjectileInteractibleHit.RemoveAllListeners();
            Destroy(gameObject);
        }
    }
}