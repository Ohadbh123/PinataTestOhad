using UnityEngine;

namespace Gameplay
{
    public enum ProjectileType
    {
        Bomb,
        Dynamite,
        RubberDuck
    }

    [CreateAssetMenu(menuName = "Config/Projectile Config File")]
    public class ProjectileConfig : ScriptableObject
    {
        public ProjectileType ProjectileType;
        public float ProjectileSpeed;
        public float ProjectileDamage;
    }
}