using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCard
{
    private Land land;
    private string symbol;

    public LandCard(Land land, string symbol)
    {
        this.land = land;
        this.symbol = symbol;
    }

    public Land getLand()
    {
        return land;
    }

    public string getSymbol()
    {
        return symbol;
    }
}
