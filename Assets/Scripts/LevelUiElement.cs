using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUiElement : MonoBehaviour
{
    public Text levelText;
    public int levelId;
    public Toggle levelToggle;
    public static event Action<LevelUiElement> OnLevelSelectionChange;

    private void OnEnable()
    {
        levelToggle.onValueChanged.AddListener(OnLevelButtonCLicked);
    }
    private void OnDisable()
    {
        levelToggle.onValueChanged.RemoveListener(OnLevelButtonCLicked);
    }

    private void OnLevelButtonCLicked(bool isOn)
    {
         OnLevelSelectionChange?.Invoke(this);
    }

    public void SetLevel(int levelnumber, bool isAvailable, ToggleGroup toggleGroup)
    {
        levelId = levelnumber;
        levelText.text = levelnumber.ToString();
        levelToggle.interactable = isAvailable;
        levelToggle.group = toggleGroup;
    }

}
