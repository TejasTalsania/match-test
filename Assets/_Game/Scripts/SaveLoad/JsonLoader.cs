using UnityEngine;

// load json file from level number
public class JsonLoader
{
    public static LevelData LoadLevel(int levelNumber)
    {
        TextAsset json = Resources.Load<TextAsset>($"Levels/level_{levelNumber}");
        return JsonUtility.FromJson<LevelData>(json.text);
    }
}