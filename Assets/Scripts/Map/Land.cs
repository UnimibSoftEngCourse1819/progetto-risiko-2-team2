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

    private string name;
    private List<Land> neighbors;
    private string nameSprite;
    private int tanksOnLand = 0;

    public Land(string name, List<Land> neighbor, string nameSprite, int tanksOnLand)
    {
        this.name = name;
        this.neighbors = neighbor;
        this.nameSprite = nameSprite;
        this.tanksOnLand = tanksOnLand;
    }

    public Land(string name, string nameSprite): this(name,  new List<Land>(), nameSprite, 0)
    {
    }

    public Land(string name,int tanks): this(name, new List<Land>(), null, tanks)
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
        return neighbors.Count == 10;
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

    public string getNameSprite(){
        return nameSprite;
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
    
    public int get_NeighborsNumber()
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