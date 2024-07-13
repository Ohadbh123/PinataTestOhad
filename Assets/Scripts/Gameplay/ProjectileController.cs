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

        public UnityAction<ProjectileController> OnProjectileInteractableHit;

        private bool _didHitAnInteractable;
        private Vector2 _direction;

        private void Start()
        {
            StartCoroutine(nameof(SetupCollider));
            
            //if didnt hit anything, clean it
            DestroyProjectile(5f);
        }
        
        //sets the direction after leaving cannon
        public void SetupProjectileDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void DestroyProjectile(float delay = 0)
        {
            Destroy(gameObject, delay);
        }

        //activate collider after leaving cannon
        private IEnumerator SetupCollider()
        {
            yield return new WaitForSeconds(.2f);
            _collider2D.enabled = true;
        }
        
        private void FixedUpdate()
        {
            //sets the forward projectile movement
            _rigidbody.velocity = _direction * _projectileSpeed;
        }

        //check which type of object was collided
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IInteractable interactable))
            {
                OnProjectileInteractableHit.Invoke(this);
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
                damageable.TakeDamage(transform);
                DestroyProjectile();
            }
        }
    }
}