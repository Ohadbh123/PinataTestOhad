using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PinataController : MonoBehaviour , IInteractable
    {
        private static readonly int _animatorHitTrigger = Animator.StringToHash("Hit");

        [SerializeField] private Animator _animator;
        
        [HideInInspector]
        public UnityEvent OnPinataAchieved;
        
        public void Interact(ProjectileController projectileController)
        {
            _animator.SetTrigger(_animatorHitTrigger);
            projectileController.DestroyProjectile();
            OnPinataAchieved.Invoke();
        }
    }
}
