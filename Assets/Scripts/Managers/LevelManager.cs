using Gameplay;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private PinataController _pinataController;
        [SerializeField] private CoinController []  _coinControllers;
        
        private int _coinCount = 0;

        private void Start()
        {
            _pinataController.OnPinataAchieved.AddListener(OnLevelCompleted);
            foreach (var controller in _coinControllers)
            {
                controller.OnStarAchieved.AddListener(OnCoinAchieved);
            }
        }

        private void OnLevelCompleted()
        {
            GameManager.Instance.UpdateStarsAmount(_coinCount);
        }
    
        public void OnLevelFailed()
        {
        
        }

        private void OnCoinAchieved()
        {
            _coinCount++;
        }
    }
}
