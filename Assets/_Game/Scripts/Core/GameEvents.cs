using System;

// Game event class to react to UI of screens...
public static class GameEvents
{
    public static Action OnResetLevel;
    public static Action OnTurnUpdated;
    public static Action OnMatchCardUpdated;
    public static Action OnComboUpdated;
    public static Action OnScoreUpdated;
    public static Action<LevelSaveData> OnLevelResumed;
    public static Action<int> OnLevelNumberUpdated;
    public static Action OnPlayButtonPressed;
    public static Action OnResumeButtonPressed;
    public static Action OnHomeButtonPressed;
    public static Action OnLevelCompleted;
    public static Action<SoundType> OnSoundRequested;

    public static void FireResetLevel()
    {
        OnResetLevel?.Invoke();
    }
    
    public static void FireTurnTaken()
    {
        OnTurnUpdated?.Invoke();
    }

    public static void FireMatchSuccess()
    {
        OnMatchCardUpdated?.Invoke();
    }

    public static void FireComboUpdate()
    {
        OnComboUpdated?.Invoke();
    }
    
    public static void FireScoreUpdate()
    {
        OnScoreUpdated?.Invoke();
    }

    public static void FireLevelResumed(LevelSaveData data)
    {
        OnLevelResumed?.Invoke(data);
    }
    
    public static void FireLevelNumberUpdated(int number)
    {
        OnLevelNumberUpdated?.Invoke(number);
    }
    
    public static void FirePlayButtonPressed()
    {
        OnPlayButtonPressed?.Invoke();
    }
    
    public static void FireResumeButtonPressed()
    {
        OnResumeButtonPressed?.Invoke();
    }
    
    public static void FireHomeButtonPressed()
    {
        OnHomeButtonPressed?.Invoke();
    }

    public static void FireLevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }
    
    public static void FireSoundRequested(SoundType type)
    {
        OnSoundRequested?.Invoke(type);
    }
}