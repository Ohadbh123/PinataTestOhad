using Gameplay.Interfaces;
using UnityEngine;

namespace Gameplay
{
    public class ObstacleController : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _damageVfx;
        public void TakeDamage()
        {
            Instantiate(_damageVfx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}