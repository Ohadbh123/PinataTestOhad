using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Config/Projectile Config File")]
    public class ProjectileConfig : ScriptableObject
    {
        public float ProjectileSpeed;
        public float ProjectileDamage;
    }
}