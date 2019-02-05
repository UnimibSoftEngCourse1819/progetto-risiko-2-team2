using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBattle : State
{
    public RectTransform UIPosition;

    public Canvas UICanvas;
    public Text numberOfTankHolder;
    private int numberOfTanks;

    public Image playerColorSprite;
    private string playerColor;

    public void Awake()
    {
        base.Awake();

        
    }

    public void SetNumberOfTanks(int i)
    {
        if (i >= 0)
        {
            numberOfTanks = i;
        }
    }

    public void ChangePlayerColor(string newColor)
    {
        playerColor = newColor;
    }
}
