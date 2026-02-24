using UnityEngine;
using System.Collections.Generic;

// Scriptable object for store card front sprites with it's related ID for match...
[CreateAssetMenu(fileName = "CardFrontSprites", menuName = "Card Match/Card Front Sprites")]
public class CardFrontSpritesData : ScriptableObject
{
    [System.Serializable]
    public class CardSpriteEntry
    {
        public int id;
        public Sprite sprite;
    }

    [SerializeField] private List<CardSpriteEntry> sprites;

    private Dictionary<int, Sprite> spriteLookup;

    public void Initialize()
    {
        spriteLookup = new Dictionary<int, Sprite>();

        foreach (var entry in sprites)
        {
            if (!spriteLookup.ContainsKey(entry.id))
                spriteLookup.Add(entry.id, entry.sprite);
        }
    }

    // this method returns correct sprite with id for level...
    public Sprite GetSprite(int id)
    {
        if (spriteLookup == null)
            Initialize();

        return spriteLookup.TryGetValue(id, out var sprite) ? sprite : null;
    }
}