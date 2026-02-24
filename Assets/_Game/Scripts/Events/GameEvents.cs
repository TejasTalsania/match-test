using System;

// Game event class to react to UI of screens...
public static class GameEvents
{
    public static Action OnTurnTaken; 
    public static Action OnMatchSuccess; 

    public static void FireTurnTaken()
    {
        OnTurnTaken?.Invoke();
    }

    public static void FireMatchSuccess()
    {
        OnMatchSuccess?.Invoke();
    }
}