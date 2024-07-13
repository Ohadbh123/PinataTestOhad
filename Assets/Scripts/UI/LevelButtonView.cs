using System;
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
        [SerializeField] private Image _buttomImage;
       
        private Color _baseColor;

        private void Awake()
        {
            _baseColor = _buttomImage.color;
        }

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

        public void SetLevelStatus(int starsAchieved)
        {
            switch (starsAchieved)
            {
                case 3:
                    _buttomImage.color = Color.green;
                    return;
                case 0:
                    ResetButton();
                    return;
                default:
                    _buttomImage.color =  Color.red;
                    break;
            }
        }

        public void ResetButton()
        {
            _buttomImage.color = _baseColor;
        }
    }
}
