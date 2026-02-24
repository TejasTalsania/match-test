using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Only generate and destroy card responsibility...
public class GridView : MonoBehaviour
{
    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform parent;
    private GridLayoutGroup _gridLayout;

    private void Awake()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
    }

    // generate new cards for new level and returns all generated cards to game manager...
    public List<CardView> SpawnCards(List<CardModel> models)
    {
        List<CardView> views = new();

        for (var i = 0; i < models.Count; i++)
        {
            var view = Instantiate(cardPrefab, parent);
            view.name = "Card_" + i;
            views.Add(view);
        }
    
        return views;
    }

    // Clears all cards from parent for new level...
    public void ResetLevel()
    {
        if (parent.childCount > 0)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(parent.GetChild(i).gameObject);
            }
        }
    }

    // Set cards grid size and adjust as per columns to perfectly fit to screen size...
    // Max 6 columns adjusted as per current code...
    public void SetRowColumn(int column)
    {
        _gridLayout.cellSize = column switch
        {
            <= 4 => new Vector2(250, 300),
            5 => new Vector2(205, 246),
            6 => new Vector2(170, 205),
            _ => _gridLayout.cellSize
        };

        _gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayout.constraintCount = column;
    }
}