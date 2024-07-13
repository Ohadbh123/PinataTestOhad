using System;
using Gameplay.Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private ProjectileConfig _projectileConfig;

        public UnityAction <ProjectileController>OnProjectileInteractableHit;

        private bool _didHitAnInteractable;
        
        private void Start()
        {
            //if didnt hit anything, clean it
            DestroyProjectile(5f);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = transform.up * _projectileConfig.ProjectileSpeed;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IInteractable interactable))
            {
                _didHitAnInteractable = true;
                interactable.Interact(this);
                return;
            }
            
            if (col.TryGetComponent(out ICollectible collectible))
            {
                collectible.Collect();
                return;
            }
            
            if (col.TryGetComponent(out IDamageable damageable))
            {
                damageable.TryTakeDamage(_projectileConfig.ProjectileDamage);
            }
        }

        public void DestroyProjectile(float delay = 0)
        {
            if (_didHitAnInteractable)
            {
                OnProjectileInteractableHit.Invoke(this);
            }

            Destroy(gameObject , delay);
        }
    }
}