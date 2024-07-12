using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class PinataController : MonoBehaviour , IInteractable
    {
        [HideInInspector]
        public UnityEvent OnPinataAchieved;
        
        public void Interact(ProjectileController projectileController)
        {
            projectileController.DestroyProjectile();
            OnPinataAchieved.Invoke();
        }

        private void OnDestroy()
        {
            OnPinataAchieved.RemoveAllListeners();
        }
    }
}
