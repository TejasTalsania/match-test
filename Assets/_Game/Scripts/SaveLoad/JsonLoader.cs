using UnityEngine;

// load json file from level number
public class JsonLoader
{
    public static LevelData LoadLevel(int levelNumber)
    {
        var levelStr = "Levels/level_" + levelNumber;
        var json = Resources.Load<TextAsset>(levelStr);
        return JsonUtility.FromJson<LevelData>(json.text);
    }
}