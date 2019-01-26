using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    /*
    MODIFICHE DA EFFETTUARE:
        -Adattare la classe quando viene implementato il game vero e proprio

        Note: NON METTETE VARIABILI CALCOLABILI come numero totale di ect; se vi serve e non 
        si può usare List.Count createvi un metodo 
    */
    
    private string nickname;
    private List<Land> territoryOwned;
    private string color;
    //private Goal goal;
   
    public Player()
    {
        nickname = null;
        territoryOwned = null;
        color = null;
    }

    public Player(string nickname, List<Land> territory, string color)//need to add Goal
    {
        this.nickname = nickname;
        territoryOwned = territory;
        this.color = color;
        //this.goal = goal;
    }

    public void addLand(Land conquered)
    {
        territoryOwned.Add(conquered);
    }

    public Player(string nickname): this(nickname, new List<Land>() , null)//need to add Goal
    {
    }

    public bool hasLand(string name)
    {
        bool result = false;

        foreach(Land territory in territoryOwned)
        {
            if(name.Equals(territory.getName()))
            {
                result = true;
            }
        }
        return result;
    }

    public int getTotalLand()
    {
        return territoryOwned.Count;
    }

}