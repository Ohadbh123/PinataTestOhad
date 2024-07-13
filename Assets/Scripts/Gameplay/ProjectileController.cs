using System.Collections;
using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] public float _projectileSpeed = 4f;

        public UnityAction <ProjectileController>OnProjectileInteractableHit;

        private bool _didHitAnInteractable;
        private Vector2 _direction;
        
        private void Start()
        {
            StartCoroutine(nameof(SetupCollider));
            //if didnt hit anything, clean it
            DestroyProjectile(5f);
        }

        private IEnumerator SetupCollider()
        {
            yield return new WaitForSeconds(.2f);
            _collider2D.enabled = true;
        }
        

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _projectileSpeed;
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
                damageable.TakeDamage();
                DestroyProjectile();
            }
        }

        public void SetupProjectileDirection(Vector2 direction)
        {
            _direction = direction;
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