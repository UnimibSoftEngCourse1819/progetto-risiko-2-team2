using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStateUI : MonoBehaviour
{
    public Text numberOfTanks;
    public Image playerColor;
    private Dictionary<string, Color> possibleColors;

    public void Awake()
    {
        possibleColors = new Dictionary<string, Color>();

        possibleColors.Add("white", new Color(1.0f, 1.0f, 1.0f));
        possibleColors.Add("blue", new Color(0.0f, 0.0f, 0.75f));
        possibleColors.Add("red", new Color(0.75f, 0.0f, 0.0f));
        possibleColors.Add("green", new Color(0.0f, 0.75f, 0.0f));
        possibleColors.Add("yellow", new Color(0.75f, 0.75f, 0.75f));
        possibleColors.Add("purple", new Color(0.75f, 0.0f, 0.75f));
        possibleColors.Add("black", new Color(0.0f, 0.0f, 0.0f));
    }

    public void changeNumber(int number)
    {
        numberOfTanks.text = number.ToString();
    }

    public void changeColor(string color)
    {
        playerColor.color = possibleColors[color];
    }
}
