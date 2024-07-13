using UnityEngine;

namespace Gameplay.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(Transform collisionPosition);
    }
}