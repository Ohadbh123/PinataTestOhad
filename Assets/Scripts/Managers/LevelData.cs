using UnityEngine;

namespace Managers
{
    [CreateAssetMenu(menuName = "Config/Level Data File")]

    public class LevelData : ScriptableObject
    {
        private int _starsCollected;

        public void UpdateStarsAmount(int amount)
        {
            if (_starsCollected < amount)
            {
                _starsCollected = amount;
            }
        }

        public int GetStarsAmount()
        {
            return _starsCollected;
        }

        public void ResetProgress()
        {
            _starsCollected = 0;
        }
    }
}