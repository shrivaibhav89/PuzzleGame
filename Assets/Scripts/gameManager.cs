using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    public List<ShapeTemp> AllEnergySource = new List<ShapeTemp>();
    public List<ShapeTemp> AllShapes = new List<ShapeTemp>();

    public List<connectors> powered = new List<connectors>();

    public List<GameObject> levelsList;
    private int activeLevel;
    public static event Action OnLevelWin;
    // Start is called before the first frame update
    private void Awake()
    {
        // Create singleton instance
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    public void ActivateLevel(int levelId)
    {
        this.activeLevel = levelId;
        levelsList[levelId - 1].SetActive(true);
        InitLevel();
    }
    public void ClearLevel()
    {
        foreach(GameObject level in levelsList)
        {
            level.SetActive(false);
        }
    }
    void InitLevel()
    {

        ShapeTemp[] all = FindObjectsOfType<ShapeTemp>();
        AllShapes = all.ToList();
        foreach (ShapeTemp temp in all)
        {


            if (temp.IsEnengySource == true)
            {

                AllEnergySource.Add(temp);
            }
        }
        checkForLoops();
    }

    // Update is called once per frame


    public void checkForLoops()
    {

        powered.Clear();
        foreach (ShapeTemp temp in AllShapes)
        {

            temp.IsGettingEnergy = false;
        }
        for (int i = 0; i < AllEnergySource.Count; i++)
        {
            for (int j = 0; j < AllEnergySource[i].Cpoints.Count; j++)
            {
                if (AllEnergySource[i].Cpoints[j].connectedTo != null)
                {
                    AllEnergySource[i].Cpoints[j].connectedTo.GetCharge();
                }

            }

        }

        int Gameover = 0;
        foreach (ShapeTemp s in AllShapes)
        {

            if (s.IsGettingEnergy == false && s.IsEnengySource == false)
            {

                Gameover++;
            }

        }

        if (Gameover == 0)
        {
            OnGameOver();
        }
    }
    public void OnLevelCompleted()
    {
        Debug.Log("Level Completed");
        SaveLevelProgress();
         OnLevelWin?.Invoke();
        // LoadNextLevel(); // Example method to load the next level
    }
    public void SaveLevelProgress()
    {
        // int currentLevel = GetCurrentLevel(); // Method to determine current level
        int highestLevel = PlayerPrefs.GetInt("HighestLevel", 0);

        if (activeLevel >= highestLevel)
        {
            PlayerPrefs.SetInt("HighestLevel", activeLevel+1);
            PlayerPrefs.Save();
            Debug.Log("Level Progress Saved: Level " + activeLevel+1);
        }
    }

    public void OnGameOver()
    {
        OnLevelCompleted();
        Debug.LogError("GameEnd");
    }
}
