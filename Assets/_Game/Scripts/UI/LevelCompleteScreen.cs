using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScreen : MonoBehaviour
{
    private int currentLevel = 1;

    private void OnEnable()
    {
        currentLevel = PlayerPrefs.GetInt(GameConfig.CurrentLevelPrefName, 1);
        currentLevel++;
        PlayerPrefs.SetInt(GameConfig.CurrentLevelPrefName, currentLevel);
    }

    public void OnNextButtonPressed()
    {
        GameEvents.FirePlayButtonPressed();
    }

    public void OnHomeButtonPressed()
    {
        GameEvents.FireHomeButtonPressed();
    }
}
