using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject buttonResume;

    private void OnEnable()
    {
        var savedData = PlayerPrefs.GetString(GameConfig.CurrentLevelDataPrefName, string.Empty);
        if (string.IsNullOrEmpty(savedData))
        {
            buttonResume.SetActive(false);
            return;
        }
        
        buttonResume.SetActive(true);
    }

    public void OnPlayButtonPressed()
    {
        GameEvents.FirePlayButtonPressed();
    }

    public void OnResumeButtonPressed()
    {
        GameEvents.FireResumeButtonPressed();
    }
}
