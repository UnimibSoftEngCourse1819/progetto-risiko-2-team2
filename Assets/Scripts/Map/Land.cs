using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PolygonCollider2D))]

public class Land 
{
    /*
    MODIFICHE DA EFFETTUARE:
        -Adattare la classe quando viene implementato il game vero e proprio

        Note: NON METTETE VARIABILI CALCOLABILI come numero totale di ect; se vi serve e non 
        si può usare List.Count createvi un metodo 
    */
    private const int MAXNEIGHBOR = 10; 

    private readonly string name;
    private readonly List<Land> neighbors;
    private int tanksOnLand = 0;

    public Land(string name, List<Land> neighbor, int tanksOnLand)
    {
        this.name = name;
        this.neighbors = neighbor;
        this.tanksOnLand = tanksOnLand;
    }

    public Land(string name): this(name,  new List<Land>(), 0)
    {
    }

    public Land(string name,int tanks): this(name, new List<Land>(), tanks)
    {
    }


    public string getName()
    {
        return name;
    }

    public List<Land> getNeighbors()
    {
        return neighbors;
    }

    public bool hasMaxNeighbor()
    {
        return neighbors.Count == MAXNEIGHBOR;
    }

    public bool isNeighbor(string name)
    {
        bool result = false;
        foreach(Land neighbor in neighbors)
        {
            if(name.Equals(neighbor.getName()))
                result = true;
        }
        return result;
    }

    public bool isNeighbor(Land land)
    {
        return isNeighbor(land.getName());
    }

    public bool addNeighbor(Land newLand)
    {
        bool result = false;
        if (! hasMaxNeighbor())
        {
            neighbors.Add(newLand);
            result = true;
        }

        return result;
    }
    
    public int getNeighborsNumber()
    {
        return neighbors.Count;
    }

    public void setTanksOnLand(int tanks)
    {
        if (tanks >= 0)
            tanksOnLand = tanks;
    }

    public int getTanksOnLand()
    {
        return tanksOnLand;
    }

    public void addTanksOnLand(int tanks)
    {
        tanksOnLand += tanks;
    }

    public void removeTanksOnLand(int tanks)
    {
        tanksOnLand -= tanks;
    }

}