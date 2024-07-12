using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;
        private int _starsAmount;
        
        #region Singleton

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();

                    if (instance == null)
                    {
                        var singletonObject = new GameObject("GameManager");
                        instance = singletonObject.AddComponent<GameManager>();
                    }
                }

                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        #endregion

        public void UpdateStarsAmount(int amount)
        {
            _starsAmount += amount;
        }
    }
}
