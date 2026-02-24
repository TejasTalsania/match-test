using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private ScreenType screenType;
    public void OnPlayButtonPressed()
    {
        GameEvents.FirePlayButtonPressed();
    }
}
