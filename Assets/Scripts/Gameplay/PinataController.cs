using Gameplay.Interfaces;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PinataController : MonoBehaviour , IInteractable
    {
        private static readonly int _animatorHitTrigger = Animator.StringToHash("Hit");

        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _hitSfx;
        [SerializeField] private AudioSource _voiceSfx;

        [HideInInspector]
        public UnityEvent OnPinataAchieved;
        
        public void Interact(ProjectileController projectileController)
        {
            _animator.SetTrigger(_animatorHitTrigger);
            projectileController.DestroyProjectile();
            AudioManager.Instance.Play(_hitSfx);
            AudioManager.Instance.PlayWithDelay(.4f,_voiceSfx);
            OnPinataAchieved.Invoke();
        }
    }
}
