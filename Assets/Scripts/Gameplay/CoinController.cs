using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class CoinController : MonoBehaviour, IInteractable
    {
        [HideInInspector]
        public UnityEvent OnStarAchieved;
        
        public void Interact(ProjectileController projectileController)
        {
            OnStarAchieved.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnStarAchieved.RemoveAllListeners();
        }
    }
}