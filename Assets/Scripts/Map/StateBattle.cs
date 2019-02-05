using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBattle : State
{
    public RectTransform UIPosition;

    public Text numberOfTankHolder;
    private int numberOfTanks;

    public Image playerColorSprite;
    private string playerColor;

    private Dictionary<string, Color> possibleColors;

    public void Awake()
    {
        base.Awake(); 
        
        possibleColors = new Dictionary<string, Color>();

        possibleColors.Add("white", new Color(1.0f, 1.0f, 1.0f));
        possibleColors.Add("blue", new Color(0.0f, 0.0f, 0.75f));
        possibleColors.Add("red", new Color(0.75f, 0.0f, 0.0f));
        possibleColors.Add("green", new Color(0.0f, 0.75f, 0.0f));
        possibleColors.Add("yellow", new Color(0.75f, 0.75f, 0.75f));
        possibleColors.Add("purple", new Color(0.75f, 0.0f, 0.75f));
        possibleColors.Add("black", new Color(0.0f, 0.0f, 0.0f));

        UIPosition.transform.position = this.transform.position;
    }

    public void SetNumberOfTanks(int i)
    {
        if (i >= 0)
        {
            numberOfTanks = i;
            numberOfTankHolder.text = numberOfTanks.ToString();
        }
    }

    public void ChangePlayerColor(string newColor)
    {
        if (possibleColors.ContainsKey(newColor))
        {
            playerColor = newColor;
            playerColorSprite.color = possibleColors[newColor];
        }
    }
}
