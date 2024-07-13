using Gameplay.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class CoinController : MonoBehaviour, ICollectible
    {
        [SerializeField] private GameObject _collectVfx;

        [HideInInspector] public UnityEvent OnStarAchieved;

        public void Collect()
        {
            Instantiate(_collectVfx, transform.position, Quaternion.identity);
            OnStarAchieved.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnStarAchieved.RemoveAllListeners();
        }
    }
}