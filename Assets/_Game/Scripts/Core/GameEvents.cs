using System;

// Game event class to react to UI of screens...
public static class GameEvents
{
    public static Action OnResetLevel;
    public static Action OnTurnTaken;
    public static Action OnMatchSuccess;
    public static Action<int> OnLevelNumberUpdated;
    public static Action OnPlayButtonPressed;
    public static Action OnHomeButtonPressed;
    public static Action OnLevelCompleted;
    public static Action OnNextButtonPressed;
    public static Action OnSettingButtonPressed;
    public static Action<SoundType> OnSoundRequested;

    public static void FireResetLevel()
    {
        OnResetLevel?.Invoke();
    }
    
    public static void FireTurnTaken()
    {
        OnTurnTaken?.Invoke();
    }

    public static void FireMatchSuccess()
    {
        OnMatchSuccess?.Invoke();
    }
    
    public static void FireLevelNumberUpdated(int number)
    {
        OnLevelNumberUpdated?.Invoke(number);
    }
    
    public static void FirePlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
    }
    
    public static void FireHomeButtonPressed()
    {
        OnHomeButtonPressed?.Invoke();
    }

    public static void FireLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    
    public static void FireNextButtonPressed()
    {
        OnNextButtonPressed?.Invoke();
    }
    
    public static void FireSettingPressed()
    {
        OnSettingButtonPressed?.Invoke();
    }
    
    public static void FireSoundRequested(SoundType type)
    {
        OnSoundRequested?.Invoke(type);
    }
}