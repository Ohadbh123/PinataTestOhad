using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private LevelButtonView[] _levelsButtons;

    private void Start()
    {
        SetupButtonListeners();
    }

    private void LoadLevel (int levelIndex)
    {
        Debug.Log(levelIndex);
    }

    private void SetupButtonListeners()
    {
        for (var i = 0; i < _levelsButtons.Length; i++)
        {
            _levelsButtons[i].SetLevelIndex(i + 1);
            _levelsButtons[i].AddButtonListener(LoadLevel , i);
        }
    }

    private void OnDestroy()
    {
        for (var i = 0; i < _levelsButtons.Length; i++)
        {
            _levelsButtons[i].RemoveButtonListener(LoadLevel , i);
        }
    }
    
    
}

