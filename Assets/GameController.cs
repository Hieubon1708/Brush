using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum BoxColor
    {
        Green, Blue, Red, Yellow
    }

    private void Awake()
    {
        instance = this;
    }

    public Color GetColor(BoxColor boxColor)
    {
        switch (boxColor)
        {
            case BoxColor.Green: return Color.green;
            case BoxColor.Blue: return Color.blue;
            case BoxColor.Red: return Color.red;
            case BoxColor.Yellow: return Color.yellow;
        }

        return Color.white;
    }
}
