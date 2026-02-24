using System.Collections.Generic;

// List shuffle utility for generate random card sequence to play...
public static class ShuffleUtility
{
    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}