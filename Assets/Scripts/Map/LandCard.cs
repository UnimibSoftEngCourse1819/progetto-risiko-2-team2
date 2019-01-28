using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCard
{
    enum Symbol { Cavalry, Infantry, Artillery, Jolly };
    private Land land;
    private Symbol symbol;

    public LandCard(Land land, int symbol)
    {
        this.land = land;
        if (symbol == 0)
            this.symbol = Symbol.Cavalry;
        else if (symbol == 1)
            this.symbol = Symbol.Infantry;
        else
            this.symbol = Symbol.Artillery;
    }

    public LandCard(Land land, string symbol)
    {
        this.land = land;
        if (symbol.Equals("Cavalry", StringComparison.OrdinalIgnoreCase))
            this.symbol = Symbol.Cavalry;
        else if (symbol.Equals("Infantry", StringComparison.OrdinalIgnoreCase))
            this.symbol = Symbol.Infantry;
        else
            this.symbol = Symbol.Artillery;
    }

    public LandCard()
    {
        symbol = Symbol.Jolly;
        land = null;
    }

    public Land getLand()
    {
        return land;
    }

    public string getSymbol()
    {
        if (symbol.Equals(Symbol.Cavalry))
            return "Cavalry";
        else if (symbol.Equals(Symbol.Infantry))
            return "Infantry";
        else
            return "Artillery";
    }

    public bool isJolly()
    {
        result = false;
        if (symbol.Equals("Jolly"))
            result = true;
        return result;
    }
}
