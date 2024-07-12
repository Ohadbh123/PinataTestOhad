using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelText;

        public void SetLevelIndex(int levelIndex)
        {
            _levelText.text = levelIndex.ToString();
        }

        public void AddButtonListener(UnityAction<int> action, int levelIndex)
        {
            _button.onClick.AddListener(delegate { action(levelIndex); });
        }
    
        public void RemoveButtonListener(UnityAction<int> action, int levelIndex)
        {
            _button.onClick.RemoveListener(delegate { action(levelIndex); });
        }
    }
}
