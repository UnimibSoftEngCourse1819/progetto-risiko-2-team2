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
    private List<LandCard> landCards;
    private string color;
    private int nTanks;
    //private Goal goal;
   
    public Player()
    {
        nickname = null;
        territoryOwned = null;
        color = null;
        nTanks = 0;
        landCards = new List<LandCard>();
    }

    public Player(string username)
    {
        nickname = username;
        territoryOwned = null;
        color = null;
        nTanks = 0;
        landCards = new List<LandCard>();
    }

    public Player(string nickname, List<Land> territory, string color, int nTanks)//need to add Goal
    {
        this.nickname = nickname;
        territoryOwned = territory;
        this.color = color;
        this.nTanks = nTanks;
        //this.goal = goal;
    }

    public void addCard(LandCard card)
    {
        landCards.Add(card);
    }

    public void addLand(Land conquered)
    {
        territoryOwned.Add(conquered);
    }

    public void removeLand(Land land)
    {
        territoryOwned.Remove(land);
    }

    public Player(string nickname, int nTanks) : this(nickname, new List<Land>() , null, nTanks)//need to add Goal
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

    public bool hasLost()
    {
        if (territoryOwned.Count == 0)
            return true;

        return false;
    }

    public List<Land> getTerritoryOwned()
    {
        return territoryOwned;
    }

    public string getColor()
    {
        return color;
    }

    public string getName()
    {
        return nickname;
    }

    public void addTanks(int nTanks)
    {
        this.nTanks += nTanks;
    }

    public void removeTanks(int nTanks)
    {
        this.nTanks -= nTanks;
    }

    public void setNTanks(int nTanks)
    {
        this.nTanks = nTanks;
    }

    public int getNTanks()
    {
        return nTanks;
    }

    public bool hasContinent(Continent continent)
    {
        List<Land> lands = continent.getLands();

        foreach(Land land in lands)
        {
            if (!hasLand(land.getName()))
                return false;
        }

        return true;
    }

    public List<LandCard> getLandCards()
    {
        return landCards;
    }

    public void addLandCard(LandCard card)
    {
        landCards.Add(card);
    }

    public void removeLandCard(List<LandCard> cards)
    {
        foreach (LandCard card in cards)
            landCards.Remove(card);
    }

    public LandCard getCard(string name)
    {
        LandCard cardFound = null;
        foreach(LandCard card in landCards)
        {
            if(card.isJolly() && name.Equals("Jolly"))
                cardFound = card;
            if(!card.isJolly() && card.getLand().Equals(name))
                cardFound = card;          
        }       
        return cardFound;
    }

    public List<string> getListCard()
    {
        List<string> nameCards = new List<string>();
        foreach(LandCard card in landCards)
        {
            if(card.isJolly())
                nameCards.Add("Jolly");
            else
                nameCards.Add(card.getSymbol() + ": " + card.getLand().getName());
        }
        return nameCards;
    }
}