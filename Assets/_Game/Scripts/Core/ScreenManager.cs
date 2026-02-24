using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenType
{
    MainMenuScreen,
    GameplayScreen,
    LevelCompleteScreen
}

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private List<Screen> allScreens = new List<Screen>();
    private Dictionary<ScreenType, BaseScreen> _screenMap;
    private BaseScreen _currentScreen;

    private void Awake()
    {
        _screenMap = new Dictionary<ScreenType, BaseScreen>();

        for (var i = 0; i < allScreens.Count; i++)
        {
            _screenMap.Add(allScreens[i].type, allScreens[i].screen);
        }
        
        _currentScreen = _screenMap[ScreenType.MainMenuScreen];
    }

    private void OnEnable()
    {
        GameEvents.OnHomeButtonPressed += OnOpenMainMenuScreen;
        GameEvents.OnPlayButtonPressed += OnPlayButtonPressed;
        GameEvents.OnResumeButtonPressed += OnPlayButtonPressed;
        GameEvents.OnLevelCompleted += ShowLevelCompleteScreen;
    }

    private void OnDisable()
    {
        GameEvents.OnHomeButtonPressed -= OnOpenMainMenuScreen;
        GameEvents.OnPlayButtonPressed -= OnPlayButtonPressed;
        GameEvents.OnResumeButtonPressed -= OnPlayButtonPressed;
        GameEvents.OnLevelCompleted -= ShowLevelCompleteScreen;
    }

    public void ShowScreen(ScreenType type)
    {
        if (_currentScreen != null)
            _currentScreen.Hide();

        if (_screenMap.TryGetValue(type, out var newScreen))
        {
            _currentScreen = newScreen;
            _currentScreen.Show();
        }
    }

    private void OnOpenMainMenuScreen()
    {
        ShowScreen(ScreenType.MainMenuScreen);
    }
    
    private void OnPlayButtonPressed()
    {
        _currentScreen.Hide();
    }
    
    private void ShowLevelCompleteScreen()
    {
        ShowScreen(ScreenType.LevelCompleteScreen);
    }
}

[Serializable]
public class Screen
{
    public ScreenType type;
    public BaseScreen screen;
}