using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    private int levelCount = 50;
    public Transform levelParent;
    [SerializeField] private ToggleGroup toggleGroup;
    public Button startGameButton;
    private int activeLevel = 1;
    public GameObject startUI;
    public List<LevelUiElement> levels;
    [SerializeField]private Button continueButton;
    [SerializeField] private GameObject gamewinUI;
    private void OnEnable()
    {
        gameManager.OnLevelWin += LevelWin;
        LevelUiElement.OnLevelSelectionChange += OnLevelSelect;
        startGameButton.onClick.AddListener(GameStart);
        continueButton.onClick.AddListener(OnCLickContinue);
    }
    private void OnDisable()
    {
         gameManager.OnLevelWin -= LevelWin;
        LevelUiElement.OnLevelSelectionChange -=OnLevelSelect;
        startGameButton.onClick.RemoveListener(GameStart);
        continueButton.onClick.RemoveListener(OnCLickContinue);
    }

    private void LevelWin()
    {
        gamewinUI.SetActive(true);
    }

    private void OnCLickContinue()
    {
        gameManager.instance.ClearLevel();
        gamewinUI.SetActive(false);
        ShowStartUI();
    }

    private void Start()
    {
        SpawnLevels();
    }
    public void SpawnLevels()
    {
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 1); // Default to level 1 if no progress saved

        for (int i = 0; i < levels.Count; i++)
        {
            bool isInteractable = (i + 1) <= highestLevel; // Only levels up to the highest unlocked are interactable
            levels[i].SetLevel(i + 1, isInteractable, toggleGroup);
        }
    }
    private void GameStart()
    {
        gameManager.instance.ActivateLevel(activeLevel);
        // GridPathGenerator.Instance.SetActiveLevel(activeLevel);
        HideStartUI();
    }
    private void OnLevelSelect(LevelUiElement level)
    {
        if (level.levelToggle.isOn)
        {
            Debug.LogError("Active Level = " + level.levelId);
            activeLevel = level.levelId;
        }
        else
        {
            activeLevel = 1;
        }
    }
    private void HideStartUI()
    {
        startUI.SetActive(false);
    }
    public void ShowStartUI()
    {
        startUI.SetActive(true);
    }
}
