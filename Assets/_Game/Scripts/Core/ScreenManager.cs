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
    [Serializable]
    public class Screen
    {
        public ScreenType type;
        public BaseScreen screen;
    }
    public static ScreenManager Instance;
    [SerializeField] private List<Screen> allScreens = new List<Screen>();
    private Dictionary<ScreenType, BaseScreen> _screenMap;
    private BaseScreen _currentScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        
        _screenMap = new Dictionary<ScreenType, BaseScreen>();

        for (var i = 0; i < allScreens.Count; i++)
        {
            _screenMap.Add(allScreens[i].type, allScreens[i].screen);
        }
        
        _currentScreen = _screenMap[ScreenType.MainMenuScreen];
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

    public void HideScreen(ScreenType type)
    {
        if (_currentScreen != null)
        {
            _currentScreen.Hide();
            _currentScreen = null;
        }
    }
}
